using System.Threading.Tasks;

namespace ReadModel.Domain.Aliases
{
    public interface IAliasSwapper
    {
        Task SwapAllIndexesAsync();
        Task SwapIndexAsync(IndexType indexType);
    }
}