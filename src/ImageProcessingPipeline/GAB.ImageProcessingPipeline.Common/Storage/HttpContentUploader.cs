using System.Net.Http;
using System.Threading.Tasks;

namespace GAB.ImageProcessingPipeline.Common.Storage
{
    public static class HttpContentUploader
    {
        public static async Task<string> UploadBlob(HttpContent content, string fileName){
            
            var originalsContainer = CloudBlobStorage.GetBlobContainerReference(BlobContainers.Originals);

            var blob = originalsContainer.GetBlockBlobReference(fileName);
            blob.Properties.ContentType = content.Headers.ContentType.MediaType;

            using (var outputStream = await content.ReadAsStreamAsync())
            {
                outputStream.Position = 0;

                await blob.UploadFromStreamAsync(outputStream);
            }

            return blob.Uri.AbsoluteUri;
        }
    }
}
