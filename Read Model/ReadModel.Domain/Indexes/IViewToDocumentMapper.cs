using System.Collections.Generic;

namespace ReadModel.Domain.Indexes
{
    public interface IViewToDocumentMapper
    {
        IReadOnlyCollection<object> Map(IReadOnlyCollection<object> views, IndexType indexType);
        object Map(object view, IndexType indexType);
    }
}