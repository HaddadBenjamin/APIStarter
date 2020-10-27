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
                document.FormattedDuration = $"{(view.Duration.Minutes * 60) + view.Duration.Seconds}.{view.Duration.ToString("fff")}s";
                document.FormattedDate = $"{view.Date.ToString("G")}.{view.Date.ToString("fff")}";
               
                document.GeoIp = new GeoIp
                {
                    IPv4 = view.IPv4,
                    Location = new GeoLocation(view.Latitude, view.Longitude)
                };
               
                document.Request = new HttpRequestRequest
                {
                    Method = view.HttpMethod,
                    Uri = view.Uri,
                    Body = view.RequestBody,
                    Headers = view.RequestHeaders,
                    UserAgent = view.UserAgent
                };
                
                document.Response = new HttpRequestResponse
                {
                    Body = view.ResponseBody,
                    HttpStatus = view.HttpStatus
                };
               
                document.Audit = new HttpRequestAudit
                {
                    ClientApplication = view.ClientApplication,
                    CorrelationId = view.CorrelationId,
                    ImpersonatedUserId = view.ImpersonatedUserId,
                    UserId = view.UserId
                };
            });
    }
}