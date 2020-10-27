using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Configuration;
using WriteModel.Domain.IpLocation;

namespace WriteModel.Infrastructure.GeoLocation
{
    public class GeoLocationResolverByIp : IGeoLocationResolverByIp
    {
        private readonly IFlurlClient _flurlClient;

        public GeoLocationResolverByIp(IFlurlClientFactory flurlClientFactory) => _flurlClient = flurlClientFactory
            .Get(@"https://freegeoip.app/json/")
            .WithHeader("Content-Type", "application/json");

        public async Task<Domain.IpLocation.GeoLocation> ResolveAsync(string ip) => await _flurlClient.Request(ip).GetJsonAsync<Domain.IpLocation.GeoLocation>();
    }
}