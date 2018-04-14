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
    public static class ColorMatrixProcessor
    {
        public const string OutputFileName = "grayscale-{name}";

        public static async Task Process(Stream imageStream, CloudBlockBlob imageBlob, string name, TraceWriter log)
        {
            log.Info($"[ColorMatrix] - Triggered for image name: {name}, size: {imageStream.Length} bytes");

            try
            {
                var sourceImage = (Bitmap)Image.FromStream(imageStream);

                var imageFormat = sourceImage.GetImageFormat();
                var mimeType = sourceImage.GetMimeType();

                var generatedImage = sourceImage.DrawAsGrayscale();
                await ImageBlobUploader.UploadBlob(generatedImage, imageFormat, mimeType, imageBlob);

                log.Info($"[ColorMatrix] - Completed for: {name}. File location: {imageBlob.Uri.AbsoluteUri}");
            }
            catch (Exception exception)
            {
                log.Error($"[ColorMatrix] - Failed: {exception.Message}", exception);
            }
        }
    }
}
