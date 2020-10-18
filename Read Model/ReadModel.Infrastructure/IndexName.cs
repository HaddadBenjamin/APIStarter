using System.Collections.Generic;
using ReadModel.Domain;
using ReadModel.Domain.Interfaces;

namespace ReadModel.Infrastructure
{
    public class IndexName : IIndexName
    {
        private readonly Dictionary<IndexType, string> _indexNames = new Dictionary<IndexType, string>
        {
            { IndexType.Item, "items"},
            { IndexType.AuditRequest, "auditrequests"},
        };

        public string GetIndexName(IndexType indexType) => _indexNames[indexType];
    }
}