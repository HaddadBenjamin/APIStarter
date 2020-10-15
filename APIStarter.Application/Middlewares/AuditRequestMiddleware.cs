using System;
using System.IO;
using System.Threading.Tasks;
using APIStarter.Domain.Audit.Commands;
using APIStarter.Domain.Audit.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;

namespace APIStarter.Application.Middlewares
{
    public class AuditRequestMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly AuditConfiguration _auditConfiguration;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public AuditRequestMiddleware(RequestDelegate requestDelegate, AuditConfiguration auditConfiguration)
        {
            _requestDelegate = requestDelegate;
            _auditConfiguration = auditConfiguration;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!_auditConfiguration.AuditRequests)
                return;

            var startTime = DateTime.UtcNow;
            var request = httpContext.Request;
            var response = httpContext.Response;
            var requestBody = await GetRequestBody(request);
            var responseBody = await GetResponseBody(httpContext, response);

            // Ignore Command attribute à mettre sur mon service d'auddit et celui ci.
            var createAuditRequest = new CreateAuditRequest
            {
                Body = requestBody,
                Headers = request.Headers,
                Duration = DateTime.UtcNow - startTime,
                Message = responseBody,
                Method = request.Method,
                Status = response.StatusCode,
                Uri = $"{request.Host}{request.Path}{request.QueryString}"
            };
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await request.Body.CopyToAsync(requestStream);

            var requestBody = ReadStreamByChunks(requestStream);

            request.Body.Position = 0;

            return requestBody;
        }

        private async Task<string> GetResponseBody(HttpContext httpContext, HttpResponse response)
        {
            await using var responseStream = _recyclableMemoryStreamManager.GetStream();

            response.Body = responseStream;

            await _requestDelegate(httpContext);

            response.Body.Seek(0, SeekOrigin.Begin);

            var responseBody = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);
            
            await responseStream.CopyToAsync(response.Body);

            return responseBody;
        }

        private static string ReadStreamByChunks(Stream stream, int readChunkBufferLength = 4096)
        {
            stream.Seek(0, SeekOrigin.Begin);
            
            using var stringWriter = new StringWriter();
            using var stringReader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            var readChunkLength = 0;

            do
            {
                readChunkLength = stringReader.ReadBlock(readChunk, 0, readChunkBufferLength);
                stringWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return stringWriter.ToString();
        }


        //    public async Task Invoke(HttpContext httpContext)
        //    {
        //        
        //        

        //        
        //        await _requestDelegate.Invoke(httpContext);

        //    }
        //}
    }
}
