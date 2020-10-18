using System.Threading.Tasks;

namespace ReadModel.ElasticSearch
{
    public interface IIndexRebuilder
    {
        Task RebuildAllAsync();
        Task RebuildAsync(IndexType indexType);
    }
}
