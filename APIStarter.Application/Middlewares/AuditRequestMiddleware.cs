using System;
using System.IO;
using System.Threading.Tasks;
using APIStarter.Domain.Audit.Commands;
using APIStarter.Domain.Audit.Configuration;
using Microsoft.AspNetCore.Http;

namespace APIStarter.Application.Middlewares
{
    public class AuditRequestMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly AuditConfiguration _auditConfiguration;

        public AuditRequestMiddleware(RequestDelegate requestDelegate, AuditConfiguration auditConfiguration)
        {
            _requestDelegate = requestDelegate;
            _auditConfiguration = auditConfiguration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!_auditConfiguration.AuditRequests)
                return;
            
            var startTime = DateTime.UtcNow;

            // Ignore Command attribute à mettre sur mon service d'auddit et celui ci.
            var request = httpContext.Request;
            var response = httpContext.Response;

            request.EnableBuffering();
            request.Body.Position = 0;
            //response.Body.Position = 0;
            using (var requestReader = new StreamReader(request.Body))
            //using (var responseReader = new StreamReader(response.Body))
            {
                var createAuditRequest = new CreateAuditRequest
                {
                    Body = await requestReader.ReadToEndAsync(),
                    Headers = request.Headers,
                    Duration = DateTime.UtcNow - startTime,
                    Message = String.Empty,//responseReader.ReadToEnd(),
                    Method = request.Method,
                    Status = response.StatusCode,
                    Uri = $"{request.Host}{request.Path}{request.QueryString}"
                };

                //await mediator.SendCommand(createAuditRequest);
            }
            await _requestDelegate.Invoke(httpContext);

        }
    }
}
