using System.Net.Http;
using System.Threading.Tasks;
using GAB.ImageProcessingPipeline.Common;
using GAB.ImageProcessingPipeline.Common.Functions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace GAB.ImageProcessingPipeline.Functions
{
    public static class IngressFunction
    {
        [FunctionName("Ingress")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestMessage req,
            TraceWriter log)
        {
            AppSettings.TrackWriterLogAll(log);

            return await IngressProcessor.Process(req, log);
        }
    }
}
