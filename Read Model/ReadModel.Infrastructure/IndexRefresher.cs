using System;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Interfaces;

namespace ReadModel.Infrastructure
{
    public class IndexRefresher : IIndexRefresher
    {
        private readonly IIndexName _indexName;
        private readonly IIndexCleaner _indexCleaner;
        private readonly IIndexRefresher _indexRefresher;
        private readonly ElasticClient _client;

        public IndexRefresher(IIndexName indexName, IReadModelClient readModelClient, IIndexCleaner indexCleaner, IIndexRefresher indexRefresher)
        {
            _indexName = indexName;
            _indexCleaner = indexCleaner;
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
            await _indexCleaner.CleanIndexAsync(indexType);

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