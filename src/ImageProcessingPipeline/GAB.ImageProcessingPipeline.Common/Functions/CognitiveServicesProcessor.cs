using System;
using System.IO;
using System.Threading.Tasks;
using GAB.ImageProcessingPipeline.Common.Storage;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace GAB.ImageProcessingPipeline.Common.Functions
{
    public static class CognitiveServicesProcessor
    {
        public const string OutputBlobPath = BlobContainers.CognitiveServices + "/" + "{name}.json";

        public static async Task Process(Stream image, CloudBlockBlob jsonBlob, string name, TraceWriter log)
        {
            log.Info($"[CognitiveServices] - Triggered for image name: {name}, size: {image.Length} bytes");

            try
            {
                image.Position = 0;
                image.Seek(0, SeekOrigin.Begin);

                var data = await GetCognitiveServicesData(image);
                var json = JsonConvert.SerializeObject(data);

                await JsonBlobUploader.UploadBlob(jsonBlob, json);

                log.Info($"[CognitiveServices] - Analysis completed for {name}. File location: {jsonBlob.Uri.AbsoluteUri}");
            }
            catch (Exception exception)
            {
                log.Error($"[CognitiveServices] - Failed: {exception.Message}", exception);
            }
        }

        private static async Task<AnalysisResult> GetCognitiveServicesData(Stream image)
        {
            // Remark: Use ApiRoot that is correct for the region in which
            // you created your SubscriptionKey!
            // Source: https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/vision-api-how-to-topics/howtosubscribe
            var subscriptionKey = AppSettings.CognitiveServicesSubscriptionKey;
            var apiRoot = AppSettings.CognitiveServicesApiRoot;

            var features = new[] { VisualFeature.Tags, VisualFeature.Categories };
            
            var visionClient = new VisionServiceClient(subscriptionKey, apiRoot);
            
            return await visionClient.AnalyzeImageAsync(image, features);
        }
    }
}
