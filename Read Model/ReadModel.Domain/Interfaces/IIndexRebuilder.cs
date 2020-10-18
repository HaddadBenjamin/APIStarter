using System.Threading.Tasks;

namespace ReadModel.Domain.Interfaces
{
    public interface IIndexRebuilder
    {
        Task RebuildAllIndexesAsync();
        Task RebuildIndexAsync(IndexType indexType);
    }
}
