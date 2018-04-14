using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using GAB.ImageProcessingPipeline.Web.Models;
using GAB.ImageProcessingPipeline.Web.Services;
using Microsoft.ApplicationInsights;

namespace GAB.ImageProcessingPipeline.Web.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Upload image for processing.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadFileAsync(UploadFileModel model)
        {
            if (ModelState.IsValid == false)
            {
                return CreateFailedResult(GetModelStateErrors());
            }

            try
            {
                var result = await IngressFunctionClient.UploadFile(model.File);

                return result
                    ? CreateSuccessResult()
                    : CreateFailedResult("Upload failed!");
            }
            catch (Exception exception)
            {
                new TelemetryClient().TrackException(exception);

                return CreateFailedResult(exception.Message + ", " + exception.StackTrace);
            }
        }

        private JsonResult CreateSuccessResult()
        {
            return Json(
                new
                {
                    success = true
                });
        }

        private JsonResult CreateFailedResult(string message)
        {
            return Json(
                new
                {
                    success = false,
                    message
                });
        }

        private string GetModelStateErrors()
        {
            var errorMessages = new List<string>();
            foreach (var modelState in ModelState.Values)
            {
                errorMessages.AddRange(modelState.Errors.Select(error => error.ErrorMessage));
            }
            return string.Join(",", errorMessages);
        }
    }
}
