using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadModel.Domain.Readers
{
    public interface IWriteModelReader
    {
        Task<IReadOnlyCollection<TEntityView>> GetAll<TEntityView>(IndexType indexType);
        Task<TEntityView> GetById<TEntityView>(IndexType indexType, Guid id);
    }
}