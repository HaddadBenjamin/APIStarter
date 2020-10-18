using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadModel.Domain.Readers
{
    public interface IWriteModelReader
    {
        Task<IReadOnlyCollection<dynamic>> GetAll(IndexType indexType);
        Task<dynamic> GetById(IndexType indexType, Guid id);
    }
}