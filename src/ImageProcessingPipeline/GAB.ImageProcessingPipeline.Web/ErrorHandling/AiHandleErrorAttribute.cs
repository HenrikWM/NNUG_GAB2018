using System;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;

namespace GAB.ImageProcessingPipeline.Web.ErrorHandling
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AiHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext?.HttpContext != null && filterContext.Exception != null)
            {
                new TelemetryClient().TrackException(filterContext.Exception);
            }
            base.OnException(filterContext);
        }
    }
}
