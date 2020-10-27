using Nest;
using ReadModel.Domain.Index.HttpRequest;
using ReadModel.Domain.WriteModel.Views;
using Profile = AutoMapper.Profile;

namespace ReadModel.Infrastructure.MappingConfigurations
{
    public class HttpRequestMappingConfiguration : Profile
    {
        public HttpRequestMappingConfiguration() => CreateMap<HttpRequestView, HttpRequest>()
            .AfterMap((view, document) =>
            {
                document.Duration = $"{(view.Duration.Minutes * 60) + view.Duration.Seconds}.{view.Duration.ToString("fff")}s";
                document.Date = $"{view.Date.ToString("G")}.{view.Date.ToString("fff")}";
                document.GeoIp = new GeoIp
                {
                    IPv4 = view.IPv4,
                    Latitude = view.Latitude,
                    Longitude = view.Longitude,
                    Location = new GeoLocation(view.Latitude, view.Longitude)
                };
            });
    }
}