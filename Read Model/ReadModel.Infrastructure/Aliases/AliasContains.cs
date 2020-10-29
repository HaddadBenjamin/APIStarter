using System.Linq;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Aliases;
using ReadModel.Domain.Clients;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Aliases
{
    public class AliasContains : IAliasContains
    {
        private readonly IIndexNameWithAlias _indexName;
        private readonly ElasticClient _client;

        public AliasContains(IReadModelClient readModelClient, IIndexNameWithAlias indexName)
        {
            _indexName = indexName;
            _client = readModelClient.ElasticClient;
        }

        public bool Contains(IndexType indexType)
        {
            var aliasName = IndexNameWithoutAlias.AliasName(IndexType.Item);
            var indexThatContainsTheAlias = _client.Indices.GetAlias(aliasName).Indices.Keys.FirstOrDefault()?.Name ?? aliasName;

            return indexThatContainsTheAlias == _indexName.IndexName(IndexType.Item);
        }
    }
}