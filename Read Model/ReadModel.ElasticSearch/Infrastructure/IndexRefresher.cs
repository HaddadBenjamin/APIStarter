using System;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using ReadModel.ElasticSearch.Domain;

namespace ReadModel.ElasticSearch.Infrastructure
{
    public class IndexRefresher : IIndexRefresher
    {
        private readonly IIndexName _indexName;
        private readonly IIndexContentDeleter _indexContentDeleter;
        private readonly IIndexRefresher _indexRefresher;
        private readonly ElasticClient _client;

        public IndexRefresher(IIndexName indexName, IReadModelClient readModelClient, IIndexContentDeleter indexContentDeleter, IIndexRefresher indexRefresher)
        {
            _indexName = indexName;
            _indexContentDeleter = indexContentDeleter;
            _indexRefresher = indexRefresher;
            _client = readModelClient.ElasticClient;
        }

        public async Task RefreshAllIndexesAsync()
        {
            var refreshIndexesTasks = ((IndexType[])Enum.GetValues(typeof(IndexType))).Select(RefreshIndexAsync).ToArray();

            Task.WaitAll(refreshIndexesTasks);
        }

        public async Task RefreshIndexAsync(IndexType indexType)
        {
            await _indexContentDeleter.DeleteIndexAsync(indexType);

            // Refresh Index.
            throw new NotImplementedException();
        }

        public Task RefreshDocumentAsync(IndexType indexType, Guid id)
        {
            // Delete document of this index
            throw new NotImplementedException();
        }
    }
}