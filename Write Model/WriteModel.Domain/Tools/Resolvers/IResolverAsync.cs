using System.Threading.Tasks;

namespace WriteModel.Domain.Tools.Resolvers
{
    public interface IResolverAsync<TResolveResult>
    {
        Task<TResolveResult> ResolveAsync();
    }

    public interface IResolverAsync<in TResolverParameter, TResolveResult>
    {
        Task<TResolveResult> ResolveAsync(TResolverParameter parameters);
    }
}