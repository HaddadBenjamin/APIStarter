using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadModel.Domain.Readers
{
    public interface IWriteModelReader
    {
        Task<IReadOnlyCollection<object>> GetAllAsync(IndexType indexType);
        Task<object> GetByIdAsync(IndexType indexType, Guid id);
    }
}