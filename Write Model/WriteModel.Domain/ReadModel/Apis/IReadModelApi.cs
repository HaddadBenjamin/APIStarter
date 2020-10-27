using System;
using System.Threading.Tasks;

namespace WriteModel.Domain.ReadModel.Apis
{
    public interface IReadModelApi
    {
        Task RefreshAllAsync();
        Task RefreshIndexAsync(IndexType indexType);
        Task RefreshDocumentAsync(IndexType indexType, Guid id);
    }
}
