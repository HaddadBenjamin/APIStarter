using Microsoft.AspNetCore.Http;
using WriteModel.Domain.Tools.Resolvers;

namespace APIStarter.Application.Resolvers
{
    public class IPv4Resolver : IIPv4Resolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILocalhostIPv4Resolver _localhostIPv4Resolver;

        public IPv4Resolver(IHttpContextAccessor httpContextAccessor, ILocalhostIPv4Resolver localhostIPv4Resolver)
        {
            _httpContextAccessor = httpContextAccessor;
            _localhostIPv4Resolver = localhostIPv4Resolver;
        }

        public string Resolve()
        {
            var IP = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            var IPv4 = IP.IsIPv4MappedToIPv6
                ? IP.MapToIPv4().ToString()
                : IP.ToString();
            var isLocalHost = IPv4 == "::1";

            IPv4 = isLocalHost ? _localhostIPv4Resolver.Resolve() : IPv4;

            return IPv4;
        }
    }
}