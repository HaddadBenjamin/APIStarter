using System.Collections.Generic;
using ReadModel.Domain;
using ReadModel.Domain.Interfaces;

namespace ReadModel.Infrastructure
{
    public class IndexName : IIndexName
    {
        private static readonly Dictionary<IndexType, string> _indexNames = new Dictionary<IndexType, string>
        {
            { IndexType.Item, "items"},
            { IndexType.HttpRequest, "httprequests"},
        };

        public string GetIndexName(IndexType indexType) => _indexNames[indexType];
    }
}