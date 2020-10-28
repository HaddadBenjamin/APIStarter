﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Clients;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Indexes
{
    public class IndexRebuilder : IIndexRebuilder
    {
        private readonly IIndexMapper _indexMapper;
        private readonly IIndexNameWithAlias _indexName;
        private readonly ElasticClient _client;

        public IndexRebuilder(IReadModelClient client, IIndexMapper indexMapper, IIndexNameWithAlias indexName)
        {
            _indexMapper = indexMapper;
            _indexName = indexName;
            _client = client.ElasticClient;
        }

        public async Task RebuildAllIndexesAsync()
        {
            var rebuildIndexesTasks = ((IndexType[])Enum.GetValues(typeof(IndexType))).Select(RebuildIndexAsync).ToArray();

            await Task.WhenAll(rebuildIndexesTasks);
        }

        public async Task RebuildIndexAsync(IndexType indexType)
        {
            var temporaryIndexName = _indexName.TemporaryIndexName(indexType);
            var indexName = _indexName.IndexName(indexType);
            var aliasName = _indexName.AliasName(indexType);

            await _client.Indices.DeleteAsync(temporaryIndexName);
            await _client.Indices.CreateAsync(indexName, createIndexDescriptor => _indexMapper
                .Map(indexType, createIndexDescriptor)
                .Aliases(descriptor => descriptor.Alias(aliasName)));
            await _client.Indices.CreateAsync(temporaryIndexName, createIndexDescriptor => _indexMapper.Map(indexType, createIndexDescriptor));
        }
    }
}