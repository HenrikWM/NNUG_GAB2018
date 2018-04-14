using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using GAB.ImageProcessingPipeline.Common.Drawing;
using GAB.ImageProcessingPipeline.Common.Storage;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GAB.ImageProcessingPipeline.Common.Functions
{
    public static class ScalerProcessor
    {
        public const string OutputFileName = "{name}";

        public static async Task Process(
            Stream imageStream,
            CloudBlockBlob largeImageBlob,
            CloudBlockBlob mediumImageBlob,
            CloudBlockBlob smallImageBlob,
            string name,
            TraceWriter log)
        {
            log.Info($"[Scaler] - Triggered for image name: {name}, size: {imageStream.Length} bytes");

            try
            {
                var sourceImage = Image.FromStream(imageStream);

                var imageFormat = sourceImage.GetImageFormat();
                var mimeType = sourceImage.GetMimeType();

                await ImageBlobUploader.UploadBlob(
                    ImageResizer.ResizeImage(sourceImage, ImageDimensions.Dimensions[ImageSize.Large]),
                    imageFormat,
                    mimeType,
                    largeImageBlob);

                await ImageBlobUploader.UploadBlob(
                    ImageResizer.ResizeImage(sourceImage, ImageDimensions.Dimensions[ImageSize.Medium]),
                    imageFormat,
                    mimeType,
                    mediumImageBlob);

                await ImageBlobUploader.UploadBlob(
                    ImageResizer.ResizeImage(sourceImage, ImageDimensions.Dimensions[ImageSize.Small]),
                    imageFormat,
                    mimeType,
                    smallImageBlob);

                log.Info($"[Scaler] - Scaling completed for name '{name}'. File locations:\n" +
                         $"\nLarge: {largeImageBlob.Uri.AbsoluteUri}" +
                         $"\nMedium: {mediumImageBlob.Uri.AbsoluteUri}" +
                         $"\nSmall: {smallImageBlob.Uri.AbsoluteUri}");
            }
            catch (Exception exception)
            {
                log.Error($"[Scaler] - Failed: {exception.Message}", exception);
            }
        }
    }
}
