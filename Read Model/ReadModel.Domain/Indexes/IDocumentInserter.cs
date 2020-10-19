using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadModel.Domain.Indexes
{
    public interface IDocumentInserter
    {
        Task InsertAsync(IReadOnlyCollection<dynamic> documents, IndexType indexType);
        Task InsertAsync(dynamic document, IndexType indexType);
    }
}