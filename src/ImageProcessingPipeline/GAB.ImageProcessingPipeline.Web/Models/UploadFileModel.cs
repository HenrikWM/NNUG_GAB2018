using System.ComponentModel.DataAnnotations;
using System.Web;
using GAB.ImageProcessingPipeline.Common;
using GAB.ImageProcessingPipeline.Web.RequestValidation;

namespace GAB.ImageProcessingPipeline.Web.Models
{
    public class UploadFileModel
    {
        [Required]
        [WithinFileSizeRange(Constants.DefaultMaxFileUploadSize, ErrorMessage = "File size must be maximum {0} bytes")] // 4MiB
        [ImageFileTypeIsSupported(ErrorMessage = "File type is not supported. Use: {0}")]
        [LargerThanMinimumImageDimenstion(50, 50, ErrorMessage = "Minimum image height and width is {0}")]
        public HttpPostedFileBase File { get; set; }
    }
}
