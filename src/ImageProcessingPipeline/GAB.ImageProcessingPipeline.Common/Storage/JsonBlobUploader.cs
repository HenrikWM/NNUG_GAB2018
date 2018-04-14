using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GAB.ImageProcessingPipeline.Common.Storage
{
    public static class JsonBlobUploader
    {
        public static async Task UploadBlob(CloudBlockBlob blob, string json)
        {
            using (var memoryStream = new MemoryStream())
            {
                var writer = new StreamWriter(memoryStream);

                writer.Write(json);
                writer.Flush();

                memoryStream.Position = 0;

                blob.Properties.ContentType = "application/json";
                await blob.UploadFromStreamAsync(memoryStream);
            }
        }
    }
}
