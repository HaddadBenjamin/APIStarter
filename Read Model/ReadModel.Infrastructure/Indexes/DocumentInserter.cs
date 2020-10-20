using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Clients;
using ReadModel.Domain.Index;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Indexes
{
    public class DocumentInserter : IDocumentInserter
    {
        private readonly ElasticClient _elasticClient;
        private readonly Dictionary<IndexType, Func<IReadOnlyCollection<object>, Task<BulkResponse>>> _documentInserters;

        public DocumentInserter(IReadModelClient client)
        {
            _elasticClient = client.ElasticClient;
            _documentInserters = new Dictionary<IndexType, Func<IReadOnlyCollection<object>, Task<BulkResponse>>>
            {
                { IndexType.HttpRequest, IndexManyAsync<HttpRequest> },
                { IndexType.Item, IndexManyAsync<Item> },
            };
        }

        public async Task InsertAsync(IReadOnlyCollection<object> documents, IndexType indexType) => await _documentInserters[indexType](documents);

        public async Task InsertAsync(object document, IndexType indexType) => await _documentInserters[indexType](new[] { document });

        private async Task<BulkResponse> IndexManyAsync<TDocument>(IReadOnlyCollection<object> documents) where TDocument : class =>
            await _elasticClient.IndexManyAsync(documents.Cast<TDocument>());
    }
}