using System.Threading.Tasks;

namespace ReadModel.Domain.Indexes
{
    public interface IIndexRebuilder
    {
        Task RebuildAllIndexesAsync();
        Task RebuildIndexAsync(IndexType indexType);
    }
}
