using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using GAB.ImageProcessingPipeline.Common;

namespace GAB.ImageProcessingPipeline.Web.RequestValidation
{
    public class ImageFileTypeIsSupportedAttribute : ValidationAttribute
    {
        private readonly string[] _imageFileFormats;
        
        public ImageFileTypeIsSupportedAttribute()
        {
            _imageFileFormats = Constants.AcceptedImageFileFormats;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file == null || file.ContentLength == 0)
                return false;

            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            return _imageFileFormats.Any(item => item == extension);
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(string.Join(",", _imageFileFormats));
        }
    }
}
