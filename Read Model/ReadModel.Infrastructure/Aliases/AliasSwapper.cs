using System;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Aliases;
using ReadModel.Domain.Clients;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Aliases
{
    public class AliasSwapper : IAliasSwapper
    {
        private readonly IIndexNameWithAlias _indexName;
        private readonly IAliasContains _aliasContains;
        private readonly ElasticClient _client;

        public AliasSwapper(IReadModelClient readModelClient, IIndexNameWithAlias indexName, IAliasContains aliasContains)
        {
            _indexName = indexName;
            _aliasContains = aliasContains;
            _client = readModelClient.ElasticClient;
        }

        public async Task SwapAllIndexesAsync()
        {
            var swapIndexesAliasesTasks = ((IndexType[])Enum.GetValues(typeof(IndexType))).Select(SwapIndexAsync).ToArray();

            await Task.WhenAll(swapIndexesAliasesTasks);
        }

        public async Task SwapIndexAsync(IndexType indexType)
        {
            var aliasName = _indexName.AliasName(indexType);
            var indexName = _indexName.IndexName(indexType);
            var temporaryIndexName = _indexName.TemporaryIndexName(indexType);
            var doesIndexContainsAlias = _aliasContains.Contains(indexType);
            var indexWithoutAlias = doesIndexContainsAlias ? temporaryIndexName : indexName;

            await _client.Indices.BulkAliasAsync(aliases =>
            {
                aliases.Remove(a => a.Alias(aliasName).Index("*"));
                return aliases.Add(a => a.Alias(aliasName).Index(indexWithoutAlias));
            });
        }
    }
}