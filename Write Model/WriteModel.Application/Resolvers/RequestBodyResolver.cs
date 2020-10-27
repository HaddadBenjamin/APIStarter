using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WriteModel.Domain.Resolvers;

namespace APIStarter.Application.Resolvers
{
    public class RequestBodyResolver : IRequestBodyResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestBodyResolver(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

        public async Task<string> ResolveAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            httpContext.Request.EnableBuffering();

            using var streamReader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();

            httpContext.Request.Body.Position = 0;

            return requestBody == string.Empty ? null : requestBody;
        }
    }
}