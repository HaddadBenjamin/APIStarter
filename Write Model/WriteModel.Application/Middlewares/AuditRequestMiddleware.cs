using System;
using System.Linq;
using System.Threading.Tasks;
using APIStarter.Application.Resolvers;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using WriteModel.Domain.Audit.Commands;
using WriteModel.Domain.Audit.Configuration;
using WriteModel.Domain.AuthentificationContext;
using WriteModel.Domain.CQRS.Interfaces;
using WriteModel.Domain.IpLocation;
using WriteModel.Domain.Resolvers;

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

        public async Task Invoke(
            HttpContext httpContext,
            IMediator mediator,
            IRequestHeaders requestHeaders,
            IIPv4Resolver IPv4Resolver,
            IRequestBodyResolver requestBodyResolver,
            IResponseBodyResolver responseBodyResolver,
            IGeoLocationResolverByIp geoLocationResolverByIp)
        {
            if (!_auditConfiguration.AuditRequests)
            {
                await _requestDelegate(httpContext);
                return;
            }

            responseBodyResolver.RequestDelegate = _requestDelegate;

            var startTime = DateTime.UtcNow;
            var request = httpContext.Request;
            var requestBody = await requestBodyResolver.ResolveAsync();
            var responseBody = await responseBodyResolver.ResolveAsync();
            var IPv4 = IPv4Resolver.Resolve();
            var geoLocation = await geoLocationResolverByIp.ResolveAsync(IPv4);

            var createAuditRequest = new CreateAuditRequest
            {
                ClientApplication = requestHeaders.ClientApplication,
                IPv4 = IPv4,
                Latitude = geoLocation.Latitude,
                Longitude = geoLocation.Longitude,

                Uri = $"{request.Host}{request.Path}{request.QueryString}",
                HttpMethod = request.Method,
                RequestBody = requestBody,
                RequestHeaders = request.Headers
                    .Where(h => !HeadersToIgnore.Contains(h.Key))
                    .ToDictionary(h => h.Key, h => string.Join(",", h.Value.Select(v => v))),
                UserAgent = request.Headers.First(e => e.Key == "User-Agent").Value.FirstOrDefault(),

                ResponseBody = responseBody,
                HttpStatus = httpContext.Response.StatusCode,

                Duration = DateTime.UtcNow - startTime,
            };

            await mediator.SendCommandAsync(createAuditRequest);
        }
    }
}
