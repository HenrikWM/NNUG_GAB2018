using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GAB.ImageProcessingPipeline.Common.Storage;
using Microsoft.Azure.WebJobs.Host;

namespace GAB.ImageProcessingPipeline.Common.Functions
{
    public static class IngressProcessor
    {
        public static async Task<HttpResponseMessage> Process(HttpRequestMessage req, TraceWriter log)
        {
            log.Info("[Ingress] - Triggered");

            if (req.Content.IsMimeMultipartContent() == false)
            {
                return req.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                var provider = await req.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());
                var file = provider.Files[0];
                var fileName = file.Headers.ContentDisposition.FileName.Trim('\"');

                DeleteExistingBlob(fileName, log);
                await UploadBlob(file, fileName, log);

                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                log.Error($"[Ingress] - Failed: {exception.Message}", exception);
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        private static void DeleteExistingBlob(string fileName, TraceWriter log)
        {
            Parallel.ForEach(
                BlobContainers.All,
                currentContainerName =>
                {
                    var result = CloudBlobStorage.DeleteBlob(currentContainerName, fileName);
                    if (result)
                    {
                        log.Info($"[Ingress] - Deleted file '{fileName}' from '{currentContainerName}'");
                    }
                });
        }

        private static async Task UploadBlob(HttpContent content, string fileName, TraceWriter log)
        {
            var blobUri = await HttpContentUploader.UploadBlob(content, fileName);

            log.Info($"[Ingress] - Completed. File location: {blobUri}");
        }
    }
}
