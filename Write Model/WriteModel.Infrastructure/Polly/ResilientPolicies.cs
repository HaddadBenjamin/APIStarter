using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;

namespace WriteModel.Infrastructure.ReadModel.Apis
{
    public static class ResilientPolicies
    {
        public static async Task ExponentialRetryPolicy(Task<HttpResponseMessage> task) => await Policy
            .HandleResult(HttpStatusCode.InternalServerError)
            .OrResult(HttpStatusCode.BadGateway)
            .OrResult(HttpStatusCode.BadRequest)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(async () => (await task).StatusCode);
    }
}