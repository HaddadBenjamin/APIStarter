using System.Threading.Tasks;

namespace ReadModel.Domain.Aliases
{
    public interface IAliasRemoval
    {
        Task RemoveAsync(IndexType indexType);
    }
}