using System;
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
        private readonly IIndexName _indexName;
        private readonly ElasticClient _client;

        public IndexRebuilder(IReadModelClient client, IIndexMapper indexMapper, IIndexName indexName)
        {
            _indexMapper = indexMapper;
            _indexName = indexName;
            _client = client.ElasticClient;
        }

        public async Task RebuildAllIndexesAsync()
        {
            var rebuildIndexesTasks = ((IndexType[])Enum.GetValues(typeof(IndexType))).Select(RebuildIndexAsync).ToArray();

            Task.WaitAll(rebuildIndexesTasks);
        }

        public async Task RebuildIndexAsync(IndexType indexType)
        {
            var indexName = _indexName.GetIndexName(indexType);

            var indexDeleteResponse = await _client.Indices.DeleteAsync(indexName);
            var indexCreateResponse = await _client.Indices.CreateAsync(indexName, createIndexDescriptor => _indexMapper.Map(indexType, createIndexDescriptor));
        }
    }
}