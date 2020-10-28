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
            var indexThatContainsTheAlias = _client.Indices.GetAlias(IndexNameWithoutAlias.AliasName(IndexType.Item)).Indices.Keys.First().Name;

            return indexThatContainsTheAlias == IndexNameWithoutAlias.IndexName(IndexType.Item);
        }
    }
}