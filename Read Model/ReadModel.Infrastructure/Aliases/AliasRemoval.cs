using System.Threading.Tasks;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Aliases;
using ReadModel.Domain.Clients;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Aliases
{
    public class AliasRemoval : IAliasRemoval
    {
        private readonly ElasticClient _client;

        public AliasRemoval(IReadModelClient client) => _client = client.ElasticClient;

        public async Task RemoveAsync(IndexType indexType)
        {
            var aliasName = IndexNameWithoutAlias.AliasName(indexType);

            await _client.Indices.BulkAliasAsync(aliases => aliases.Remove(a => a.Alias(aliasName).Index("*")));
        }
    }
}