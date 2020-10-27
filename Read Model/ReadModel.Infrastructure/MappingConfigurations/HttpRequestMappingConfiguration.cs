using Nest;
using ReadModel.Domain.Index.HttpRequest;
using ReadModel.Domain.WriteModel.Views;
using UAParser;
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

                var userAgent = Parser.GetDefault().Parse(view.UserAgent);
                document.Request = new HttpRequestRequest
                {
                    Method = view.HttpMethod,
                    Uri = view.Uri,
                    Body = view.RequestBody,
                    Headers = view.RequestHeaders,
                    Os = userAgent.OS.Family,
                    Device = userAgent.Device.Family,
                    Browser = userAgent.UserAgent.Family
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