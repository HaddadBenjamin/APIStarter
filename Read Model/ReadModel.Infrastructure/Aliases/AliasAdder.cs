using System.Threading.Tasks;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Aliases;
using ReadModel.Domain.Clients;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Aliases
{
    public class AliasAdder : IAliasAdder
    {
        private readonly ElasticClient _client;

        public AliasAdder(IReadModelClient client) => _client = client.ElasticClient;

        public async Task AddAsync(IndexType indexType, string indexName)
        {
            var aliasName = IndexNameWithoutAlias.AliasName(indexType);

            await _client.Indices.BulkAliasAsync(aliases => aliases.Add(a => a.Alias(aliasName).Index(indexName)));
        }
    }
}