using AutoMapper;
using ReadModel.Domain.Index;
using ReadModel.Domain.WriteModel.Views;

namespace ReadModel.Infrastructure.MappingConfigurations
{
    public class HttpRequestMappingConfiguration : Profile
    {
        public HttpRequestMappingConfiguration() => CreateMap<HttpRequestView, HttpRequest>()
            .AfterMap((view, document) =>
            {
                document.Duration = $"{(view.Duration.Minutes * 60) + view.Duration.Seconds}.{view.Duration.ToString("fff")}s";
                document.Date = $"{view.Date.ToString("G")}.{view.Date.ToString("fff")}";
            });
    }
}