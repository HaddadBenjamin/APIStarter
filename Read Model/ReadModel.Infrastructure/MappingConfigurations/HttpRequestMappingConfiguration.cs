using AutoMapper;
using ReadModel.Domain.Index;
using ReadModel.Domain.WriteModel.Views;

namespace ReadModel.Infrastructure.MappingConfigurations
{
    public class HttpRequestMappingConfiguration : Profile
    {
        public HttpRequestMappingConfiguration() => CreateMap<HttpRequestView, HttpRequest>();
    }
}