using System;
using System.Threading.Tasks;

namespace ReadModel.ElasticSearch
{
    public interface IIndexRefresher
    {
        Task RefreshAllAsync();
        Task RefreshAsync(IndexType indexType);
        Task RefreshAsync(IndexType indexType, Guid id);
    }
}