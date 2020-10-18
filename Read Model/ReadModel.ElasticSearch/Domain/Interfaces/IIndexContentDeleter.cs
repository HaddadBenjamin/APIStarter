using System.Threading.Tasks;

namespace ReadModel.ElasticSearch.Domain.Interfaces
{
    public interface IIndexContentDeleter
    {
        Task DeleteIndexAsync(IndexType indexType);
    }
}