using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadModel.Domain.Indexes
{
    public interface IIndexDocumentInserter
    {
        Task InsertAsync(IReadOnlyCollection<object> documents, IndexType indexType);
        Task InsertAsync(object document, IndexType indexType);
    }
}