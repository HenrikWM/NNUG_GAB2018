using System.IO;
using System.Threading.Tasks;
using GAB.ImageProcessingPipeline.Common;
using GAB.ImageProcessingPipeline.Common.Functions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GAB.ImageProcessingPipeline.Functions
{
    public static class ColorMatrixLargeFunction
    {
        [FunctionName("ColorMatrixLarge")]
        public static async Task Run(
            [BlobTrigger(BlobContainers.ScaledLarge + "/{name}", Connection = Constants.BlobStorageConnectionName)] Stream imageStream,
            [Blob(BlobContainers.ColorMatrixLarge + "/" + ColorMatrixProcessor.OutputFileName, FileAccess.ReadWrite)] CloudBlockBlob imageBlob,
            string name,
            TraceWriter log)
        {
            AppSettings.TrackWriterLogAll(log);

            await ColorMatrixProcessor.Process(imageStream, imageBlob, name, log);
        }
    }
}
