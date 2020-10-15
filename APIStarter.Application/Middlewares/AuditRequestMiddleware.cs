using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using APIStarter.Application.Extensions;
using APIStarter.Domain.Audit.Commands;
using APIStarter.Domain.Audit.Services;
using APIStarter.Domain.AuthentificationContext;
using APIStarter.Domain.CQRS.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace APIStarter.Application.Middlewares
{
    public class AuditRequestMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IServiceScope _serviceScope;
        private readonly IMediator _mediator;
        private readonly IAuditSerializer _auditSerializer;

        public AuditRequestMiddleware(RequestDelegate requestDelegate, IServiceScopeFactory serviceScopeFactory)
        {
            _requestDelegate = requestDelegate;
            _serviceScope = serviceScopeFactory.CreateScope();
            _mediator = _serviceScope.ServiceProvider.GetService<IMediator>();
            _auditSerializer = _serviceScope.ServiceProvider.GetService<IAuditSerializer>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var request = httpContext.Request;

            if (request.Headers.ContainsKey("RequestStartTime"))
            {
                var startTime = DateTime.Parse(request.GetHeaderOrDefault("RequestStartTime"));
                var endTime = DateTime.UtcNow;

                var headers = _auditSerializer.Serialize(request.Headers);
                var response = httpContext.Response;
               
                request.Body.Seek(0, SeekOrigin.Begin);
                response.Body.Seek(0, SeekOrigin.Begin);
                using (var requestReader = new StreamReader(request.Body))
                using (var responseReader = new StreamReader(response.Body))
                {
                    var createAuditRequest = new CreateAuditRequest
                    {
                        Body = requestReader.ReadToEnd(),
                        Headers = request.Headers,
                        Duration = endTime - startTime,
                        Message = responseReader.ReadToEnd(),
                        Method = request.Method,
                        Status = response.StatusCode,
                        Uri = request.GetDisplayUrl()
                    };

                    _mediator.SendCommand(createAuditRequest);
                }

                // Ignore Command attribute à mettre sur mon service d'auddit et celui ci.
                request.Headers.Remove("RequestStartTime");
            }
            else
                request.Headers.Add("RequestStartTime", new StringValues(DateTime.UtcNow.ToString()));
 
            _requestDelegate.Invoke(httpContext);
        }
    }
}
