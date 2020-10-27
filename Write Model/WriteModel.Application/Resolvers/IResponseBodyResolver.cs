using Microsoft.AspNetCore.Http;
using WriteModel.Domain.Tools.Resolvers;

namespace APIStarter.Application.Resolvers
{
    public interface IResponseBodyResolver : IResolverAsync<string>
    {
        RequestDelegate RequestDelegate { get; set; }
    }
}