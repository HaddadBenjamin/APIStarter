using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;

namespace WriteModel.Infrastructure.ReadModel.Apis
{
    public static class ResilientPolicies
    {
        public static async Task<(HttpStatusCode statusCode, HttpRequestMessage requestMessage)> ExponentialRetryPolicy(Task<HttpResponseMessage> task, int numberOfRetries = 6)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.OK;
            HttpRequestMessage httpRequestMessage = null;

            await Policy
                .HandleResult(HttpStatusCode.InternalServerError)
                .OrResult(HttpStatusCode.BadGateway)
                .OrResult(HttpStatusCode.BadRequest)
                .WaitAndRetryAsync(numberOfRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    var httpResponseMessage = await task;

                    httpStatusCode = httpResponseMessage.StatusCode;
                    httpRequestMessage = httpResponseMessage.RequestMessage;

                    return httpStatusCode;
                });

            return (httpStatusCode, httpRequestMessage);
        }
    }
}