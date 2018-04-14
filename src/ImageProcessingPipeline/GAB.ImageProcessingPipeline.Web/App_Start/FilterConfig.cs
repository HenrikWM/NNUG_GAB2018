using System.Web.Mvc;
using GAB.ImageProcessingPipeline.Web.ErrorHandling;

namespace GAB.ImageProcessingPipeline.Web
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AiHandleErrorAttribute());
        }
    }
}
