using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GAB.ImageProcessingPipeline.Common.Storage
{
    public static class ImageBlobUploader
    {
        public static async Task UploadBlob(
            Image image,
            ImageFormat imageFormat,
            string mimeType,
            CloudBlockBlob targetBlob)
        {
            using (var outputStream = new MemoryStream())
            {
                image.Save(outputStream, imageFormat);

                outputStream.Position = 0;

                targetBlob.Properties.ContentType = mimeType;
                await targetBlob.UploadFromStreamAsync(outputStream);
            }
        }
    }
}
