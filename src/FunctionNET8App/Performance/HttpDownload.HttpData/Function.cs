using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace HttpDownload.HttpData
{
    public class Function
    {
        private const string BlobName = "dotnet-sdk-8.0.100-win-x64.zip";

        [Function("Function")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            [BlobInput($"runtimes/{BlobName}")] Stream blobStream)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/octet-stream");

            response.Body = blobStream;

            return response;
        }
    }
}
