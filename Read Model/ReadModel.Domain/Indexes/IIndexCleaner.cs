using System;
using System.Threading.Tasks;

namespace ReadModel.Domain.Indexes
{
    public interface IIndexCleaner
    {
        Task CleanIndexAsync(IndexType indexType);
        Task CleanIndexAsync(IndexType indexType, Guid id);
    }
}