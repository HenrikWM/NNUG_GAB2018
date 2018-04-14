using System.IO;
using System.Threading.Tasks;
using GAB.ImageProcessingPipeline.Common;
using GAB.ImageProcessingPipeline.Common.Functions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GAB.ImageProcessingPipeline.Functions
{
    public static class ScalerFunction
    {
        [FunctionName("Scaler")]
        public static async Task Run(
            [BlobTrigger(BlobContainers.Originals + "/{name}", Connection = Constants.BlobStorageConnectionName)] Stream imageStream,
            [Blob(BlobContainers.ScaledLarge + "/" + ScalerProcessor.OutputFileName, FileAccess.ReadWrite)] CloudBlockBlob largeImageBlob,
            [Blob(BlobContainers.ScaledMedium + "/" + ScalerProcessor.OutputFileName, FileAccess.ReadWrite)] CloudBlockBlob mediumImageBlob,
            [Blob(BlobContainers.ScaledSmall + "/" + ScalerProcessor.OutputFileName, FileAccess.ReadWrite)] CloudBlockBlob smallImageBlob,
            string name,
            TraceWriter log)
        {
            AppSettings.TrackWriterLogAll(log);

            await ScalerProcessor.Process(imageStream, largeImageBlob, mediumImageBlob, smallImageBlob, name, log);
        }
    }
}
