using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using GAB.ImageProcessingPipeline.Common;
using Microsoft.ApplicationInsights;

namespace GAB.ImageProcessingPipeline.Web.Services
{
    public static class IngressFunctionClient
    {
        private static readonly HttpClient Client = new HttpClient();

        private static readonly string Url = AppSettings.IngressFunctionBaseUrl + "api/ingress";

        public static async Task<bool> UploadFile(HttpPostedFileBase file)
        {
            file.InputStream.Position = 0;

            var content = new ByteArrayContent(ReadFully(file.InputStream));
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
            
            Client.DefaultRequestHeaders.Add("x-functions-key", AppSettings.FunctionsKey);
            var requestContent = new MultipartFormDataContent { { content, "file", file.FileName } };

            var result = await Client.PostAsync(Url, requestContent);
            if (result.IsSuccessStatusCode == false)
            {
                new TelemetryClient().TrackTrace(
                    "UploadFile: received unsuccessful status code " +
                    $"'{result.StatusCode}' when calling '{Url}' with header " +
                    $"'x-functions-key: {AppSettings.FunctionsKey}'");
            }

            return result.IsSuccessStatusCode;
        }

        // https://stackoverflow.com/a/221941
        private static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
