using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Clients;
using ReadModel.Domain.Index.HttpRequest;
using ReadModel.Domain.Index.Item;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Indexes
{
    public class DocumentInserter : IDocumentInserter
    {
        private readonly IIndexNameWithAlias _indexName;
        private readonly ElasticClient _client;
        private readonly Dictionary<IndexType, Func<IReadOnlyCollection<object>, IndexType, Task<BulkResponse>>> _documentInserters;

        public DocumentInserter(IReadModelClient client, IIndexNameWithAlias indexName)
        {
            _indexName = indexName;
            _client = client.ElasticClient;
            _documentInserters = new Dictionary<IndexType, Func<IReadOnlyCollection<object>, IndexType, Task<BulkResponse>>>
            {
                { IndexType.HttpRequest, IndexManyAsync<HttpRequest> },
                { IndexType.Item, IndexManyAsync<Item> },
            };
        }

        public async Task InsertAsync(IReadOnlyCollection<object> documents, IndexType indexType) => await _documentInserters[indexType](documents, indexType);
        public async Task InsertAsync(object document, IndexType indexType) => await _documentInserters[indexType](new[] { document }, indexType);

        private async Task<BulkResponse> IndexManyAsync<TDocument>(IReadOnlyCollection<object> documents, IndexType indexType) where TDocument : class =>
            await _client.IndexManyAsync(documents.Cast<TDocument>(), _indexName.TemporaryIndexName(indexType));
    }
}