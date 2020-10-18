using System.Threading.Tasks;

namespace ReadModel.ElasticSearch.Domain
{
    public interface IIndexContentDeleter
    {
        Task DeleteIndexAsync(IndexType indexType);
    }
}