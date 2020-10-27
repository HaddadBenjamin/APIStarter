using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;

namespace APIStarter.Application.Resolvers
{
    public class ResponseBodyResolver : IResponseBodyResolver
    {
        public RequestDelegate RequestDelegate { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();

        public ResponseBodyResolver(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public async Task<string> ResolveAsync()
        {
            if (RequestDelegate is null)
                throw new ArgumentNullException(nameof(RequestDelegate));

            var httpContext = _httpContextAccessor.HttpContext;
            var response = httpContext.Response;
            await using var responseStream = _recyclableMemoryStreamManager.GetStream();
            var originalBody = response.Body;

            response.Body = responseStream;

            await RequestDelegate(httpContext);

            responseStream.Position = 0;

            using var streamReader = new StreamReader(responseStream);
            var responseBody = streamReader.ReadToEnd();

            responseStream.Position = 0;

            await responseStream.CopyToAsync(originalBody);
            response.Body = originalBody;

            return responseBody == string.Empty ? null : responseBody;
        }
    }
}