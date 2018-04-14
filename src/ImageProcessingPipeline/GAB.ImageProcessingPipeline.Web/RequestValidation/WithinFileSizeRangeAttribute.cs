using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GAB.ImageProcessingPipeline.Web.RequestValidation
{
    public class WithinFileSizeRangeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public WithinFileSizeRangeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file == null || file.ContentLength == 0)
                return false;

            return file.ContentLength <= _maxFileSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(_maxFileSize.ToString());
        }
    }
}
