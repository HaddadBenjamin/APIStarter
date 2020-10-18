using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadModel.Domain.WriteModel.Readers
{
    public interface IWriteModelReader<TEntityView>
    {
        Task<IReadOnlyCollection<TEntityView>> GetAll();
        Task<TEntityView> GetById(Guid id);
    }
}