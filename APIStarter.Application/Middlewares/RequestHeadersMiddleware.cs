using System;
using System.Threading.Tasks;
using APIStarter.Application.Extensions;
using APIStarter.Domain.AuthentificationContext;
using Microsoft.AspNetCore.Http;

namespace APIStarter.Application.Middlewares
{
    public class RequestHeadersMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public RequestHeadersMiddleware(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

        public async Task Invoke(HttpContext httpContext, IRequestHeaders requestHeaders)
        {
            var request = httpContext.Request;

            requestHeaders.ClientApplication = request.GetHeaderOrDefault("clientApplication", "UNKNOWN");
            requestHeaders.CorrelationId = Guid.Parse(request.GetHeaderOrDefault("correlationId", Guid.NewGuid().ToString()));
            requestHeaders.UserEmail = request.GetHeaderOrDefault("userEmail");
            requestHeaders.ImpersonatedUserEmail = request.GetHeaderOrDefault("impersonatedUserEmail");
            requestHeaders.ReadVersion = request.GetHeaderOrDefault("readVersion", "v1");
            requestHeaders.WriteVersion = request.GetHeaderOrDefault("writeVersion", "v1");
        }
    }
}