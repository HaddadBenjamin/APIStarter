using System.Threading.Tasks;

namespace ReadModel.ElasticSearch.Domain
{
    public interface IIndexRebuilder
    {
        Task RebuildAllIndexesAsync();
        Task RebuildIndexAsync(IndexType indexType);
    }
}
