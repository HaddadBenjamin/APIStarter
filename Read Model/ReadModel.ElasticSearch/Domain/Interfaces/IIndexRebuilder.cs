using System.Threading.Tasks;

namespace ReadModel.ElasticSearch.Domain.Interfaces
{
    public interface IIndexRebuilder
    {
        Task RebuildAllIndexesAsync();
        Task RebuildIndexAsync(IndexType indexType);
    }
}
