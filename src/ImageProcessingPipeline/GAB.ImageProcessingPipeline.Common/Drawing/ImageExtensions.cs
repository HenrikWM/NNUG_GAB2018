using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace GAB.ImageProcessingPipeline.Common.Drawing
{
    public static class ImageExtensions
    {
        public static string GetMimeType(this Image image)
        {
            return image.RawFormat.GetMimeType();
        }

        private static string GetMimeType(this ImageFormat imageFormat)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.First(codec => codec.FormatID == imageFormat.Guid).MimeType;
        }

        public static ImageFormat GetImageFormat(this Image image)
        {
            if (image.RawFormat.Equals(ImageFormat.Jpeg))
            {
                return ImageFormat.Jpeg;
            }
            if (image.RawFormat.Equals(ImageFormat.Png))
            {
                return ImageFormat.Png;
            }
            if (image.RawFormat.Equals(ImageFormat.Gif))
            {
                return ImageFormat.Gif;
            }
            if (image.RawFormat.Equals(ImageFormat.Gif))
            {
                return ImageFormat.Gif;
            }

            throw new NotSupportedException($"Image format '{image.RawFormat}' is not supported");
        }
    }
}
