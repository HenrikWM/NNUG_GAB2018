using System.IO;
using System.Threading.Tasks;
using GAB.ImageProcessingPipeline.Common;
using GAB.ImageProcessingPipeline.Common.Functions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GAB.ImageProcessingPipeline.Functions
{
    public static class ExifFunction
    {
        [FunctionName("Exif")]
        public static async Task Run(
            [BlobTrigger(BlobContainers.Originals + "/{name}", Connection = Constants.BlobStorageConnectionName)] Stream image,
            [Blob(ExifProcessor.OutputBlobPath, FileAccess.ReadWrite)] CloudBlockBlob jsonBlob,
            string name,
            TraceWriter log)
        {
            AppSettings.TrackWriterLogAll(log);

            await ExifProcessor.Process(image, jsonBlob, name, log);
        }
    }
}
