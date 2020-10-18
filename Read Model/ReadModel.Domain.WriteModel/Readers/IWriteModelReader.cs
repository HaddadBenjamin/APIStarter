using System;
using System.Collections.Generic;

namespace ReadModel.Domain.WriteModel.Readers
{
    public interface IWriteModelReader<TIndex>
    {
        IReadOnlyCollection<TIndex> GetAll();
        IReadOnlyCollection<TIndex> GetById(Guid id);
    }
}