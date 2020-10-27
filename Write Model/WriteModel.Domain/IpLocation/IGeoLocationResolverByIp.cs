using WriteModel.Domain.Tools.Resolvers;

namespace WriteModel.Domain.IpLocation
{
    public interface IGeoLocationResolverByIp : IResolverAsync<string, GeoLocation> { }
}