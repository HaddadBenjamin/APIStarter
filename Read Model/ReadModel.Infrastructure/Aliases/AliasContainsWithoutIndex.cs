using System.Linq;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Aliases;
using ReadModel.Domain.Clients;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Aliases
{
    public class AliasContainsWithoutIndex : IAliasContainsWithoutIndex
    {
        private readonly ElasticClient _client;

        public AliasContainsWithoutIndex(IReadModelClient readModelClient) => _client = readModelClient.ElasticClient;

        public bool Contains(IndexType indexType)
        {
            var aliasName = IndexNameWithoutAlias.AliasName(IndexType.Item);
            var indexName = IndexNameWithoutAlias.IndexName(IndexType.Item);
            var indexThatContainsTheAlias = _client.Indices.GetAlias(aliasName).Indices.Keys.FirstOrDefault()?.Name ?? indexName;

            return indexThatContainsTheAlias == indexName;
        }
    }
}