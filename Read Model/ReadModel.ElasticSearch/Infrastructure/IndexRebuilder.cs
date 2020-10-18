using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using ReadModel.ElasticSearch.Domain;

namespace ReadModel.ElasticSearch.Infrastructure
{
    public class IndexRebuilder : IIndexRebuilder
    {
        private readonly IIndexMapper _indexMapper;
        private readonly ElasticClient _client;
        private Dictionary<IndexType, string> _indexNames = new Dictionary<IndexType, string>
        {
            {IndexType.Item, "items"},
            {IndexType.AuditRequest, "auditrequests"},
        };

        public IndexRebuilder(IReadModelClient client, IIndexMapper indexMapper)
        {
            _indexMapper = indexMapper;
            _client = client.ElasticClient;
        }

        public async Task RebuildAllIndexesAsync()
        {
            var refreshTasks = ((IndexType[])Enum.GetValues(typeof(IndexType))).Select(RebuildIndexAsync).ToArray();

            Task.WaitAll(refreshTasks);
        }

        public async Task RebuildIndexAsync(IndexType indexType)
        {
            var indexName = _indexNames[indexType];

            var indexDeleteResponse = await _client.Indices.DeleteAsync(indexName);
            var indexCreateResponse = await _client.Indices.CreateAsync(indexName, createIndexDescriptor => _indexMapper.Map(indexType, createIndexDescriptor));
        }
    }
}