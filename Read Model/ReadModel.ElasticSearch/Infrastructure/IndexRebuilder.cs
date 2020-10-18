using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace ReadModel.ElasticSearch
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

        public async Task RebuildAllAsync()
        {
            var refreshTasks = ((IndexType[])Enum.GetValues(typeof(IndexType))).Select(RebuildAsync).ToArray();

            Task.WaitAll(refreshTasks);
        }

        public async Task RebuildAsync(IndexType indexType)
        {
            var indexName = _indexNames[indexType];

            var indexDeleteResponse = await _client.Indices.DeleteAsync(indexName);
            var indexCreateResponse = await _client.Indices.CreateAsync(indexName, createIndexDescriptor => _indexMapper.Map(indexType, createIndexDescriptor));
        }
    }
}