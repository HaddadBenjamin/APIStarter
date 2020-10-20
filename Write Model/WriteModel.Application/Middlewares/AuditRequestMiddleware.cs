using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using WriteModel.Domain.Audit.Commands;
using WriteModel.Domain.Audit.Configuration;
using WriteModel.Domain.AuthentificationContext;
using WriteModel.Domain.CQRS.Interfaces;

namespace APIStarter.Application.Middlewares
{
    public class AuditRequestMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly AuditConfiguration _auditConfiguration;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private static readonly string[] HeadersToIgnore = { "Cookie", "Authorization", "Content-Length", "Accept", "Host", "User-Agent" };

        public AuditRequestMiddleware(RequestDelegate requestDelegate, AuditConfiguration auditConfiguration)
        {
            _requestDelegate = requestDelegate;
            _auditConfiguration = auditConfiguration;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext httpContext, IMediator mediator, IRequestHeaders requestHeaders)
        {
            if (!_auditConfiguration.AuditRequests)
            {
                await _requestDelegate(httpContext);
                return;
            }

            var startTime = DateTime.UtcNow;
            var request = httpContext.Request;
            var response = httpContext.Response;
            var requestBody = await GetRequestBody(request);
            var responseBody = await GetResponseBody(httpContext, response);

            var createAuditRequest = new CreateAuditRequest
            {
                RequestBody = requestBody,
                RequestHeaders = request.Headers
                    .Where(h => !HeadersToIgnore.Contains(h.Key))
                    .ToDictionary(h => h.Key, h => string.Join(",", h.Value.Select(v => v))),
                Duration = DateTime.UtcNow - startTime,
                ResponseBody = responseBody,
                HttpMethod = request.Method,
                HttpStatus = response.StatusCode,
                Uri = $"{request.Host}{request.Path}{request.QueryString}",
                ClientApplication = requestHeaders.ClientApplication
            };

            await mediator.SendCommandAsync(createAuditRequest);
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await request.Body.CopyToAsync(requestStream);

            using var streamReader = new StreamReader(requestStream);
            var requestBody = streamReader.ReadToEnd();

            request.Body.Position = 0;

            return requestBody == "" ? null : requestBody;
        }

        private async Task<string> GetResponseBody(HttpContext httpContext, HttpResponse response)
        {
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            var responseStream = response.Body;

            response.Body = requestStream;

            await _requestDelegate(httpContext);

            requestStream.Position = 0;

            using var streamReader = new StreamReader(requestStream);
            var responseBody = streamReader.ReadToEnd();

            requestStream.Position = 0;

            await requestStream.CopyToAsync(responseStream);
            response.Body = responseStream;

            return responseBody == "" ? null : responseBody;
        }
    }
}
