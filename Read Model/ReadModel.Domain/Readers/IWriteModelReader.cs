using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadModel.Domain.Readers
{
    public interface IWriteModelReader
    {
        Task<IReadOnlyCollection<dynamic>> GetAllAsync(IndexType indexType);
        Task<dynamic> GetByIdAsync(IndexType indexType, Guid id);
    }
}