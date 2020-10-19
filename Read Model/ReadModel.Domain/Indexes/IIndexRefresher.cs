using System;
using System.Threading.Tasks;

namespace ReadModel.Domain.Indexes
{
    public interface IIndexRefresher
    {
        Task RefreshAllIndexesAsync();
        Task RefreshIndexAsync(IndexType indexType);
        Task RefreshDocumentAsync(IndexType indexType, Guid id);
    }
}