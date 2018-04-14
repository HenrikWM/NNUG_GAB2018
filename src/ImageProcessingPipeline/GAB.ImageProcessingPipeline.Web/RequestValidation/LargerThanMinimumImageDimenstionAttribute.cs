using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web;
using GAB.ImageProcessingPipeline.Common.Drawing;

namespace GAB.ImageProcessingPipeline.Web.RequestValidation
{
    public class LargerThanMinimumImageDimenstionAttribute : ValidationAttribute
    {
        private readonly int _minimumHeight;
        private readonly int _minumumWidth;

        public LargerThanMinimumImageDimenstionAttribute(int minimumHeight, int minumumWidth)
        {
            
            _minimumHeight = minimumHeight;
            _minumumWidth = minumumWidth;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file == null || file.ContentLength == 0)
                return false;

            var image = Image.FromStream(file.InputStream);
            return image.Height > ImageDimensions.Dimensions[ImageSize.Minimum].Height &&
                   image.Width > ImageDimensions.Dimensions[ImageSize.Minimum].Width;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage($"{_minimumHeight}x{_minumumWidth}");
        }
    }
}
