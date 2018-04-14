using System.IO;
using System.Threading.Tasks;
using GAB.ImageProcessingPipeline.Common;
using GAB.ImageProcessingPipeline.Common.Functions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GAB.ImageProcessingPipeline.Functions
{
    public static class CognitiveServicesFunction
    {
        [FunctionName("CognitiveServices")]
        public static async Task Run(
            [BlobTrigger(BlobContainers.Originals + "/{name}", Connection = Constants.BlobStorageConnectionName)] Stream image,
            [Blob(CognitiveServicesProcessor.OutputBlobPath, FileAccess.ReadWrite)] CloudBlockBlob jsonBlob,
            string name,
            TraceWriter log)
        {
            AppSettings.TrackWriterLogAll(log);

            await CognitiveServicesProcessor.Process(image, jsonBlob, name, log);
        }
    }
}
