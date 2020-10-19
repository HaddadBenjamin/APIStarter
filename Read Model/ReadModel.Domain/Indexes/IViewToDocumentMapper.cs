using System.Collections.Generic;

namespace ReadModel.Domain.Indexes
{
    public interface IViewToDocumentMapper
    {
        IReadOnlyCollection<dynamic> Map(IReadOnlyCollection<dynamic> views, IndexType indexType);
        dynamic Map(dynamic view, IndexType indexType);
    }
}