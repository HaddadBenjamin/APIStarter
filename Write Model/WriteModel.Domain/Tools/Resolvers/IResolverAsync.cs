using System.Threading.Tasks;

namespace WriteModel.Domain.Tools.Resolvers
{
    public interface IResolverAsync<ResolveResult>
    {
        Task<ResolveResult> ResolveAsync();
    }
}