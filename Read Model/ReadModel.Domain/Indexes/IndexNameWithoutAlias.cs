using System.Collections.Generic;

namespace ReadModel.Domain.Indexes
{
    public static class IndexNameWithoutAlias
    {
        private static readonly Dictionary<IndexType, string> _indexNames = new Dictionary<IndexType, string>
        {
            { IndexType.Item, "items"},
            { IndexType.HttpRequest, "httprequests"},
        };

        public static string AliasName(IndexType indexType) => _indexNames[indexType];
        public static string IndexName(IndexType indexType) => $"{_indexNames[indexType]}_1";
        public static string TemporaryIndexName(IndexType indexType) => $"{_indexNames[indexType]}_2";
    }
}