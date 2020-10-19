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

        public DocumentInserter(IReadModelClient client) => _elasticClient = client.ElasticClient;

        public async Task InsertAsync(IReadOnlyCollection<dynamic> documents, IndexType indexType)
        {
            switch (indexType)
            {
                case IndexType.HttpRequest: await IndexManyAsync<HttpRequest>(documents); break;
                case IndexType.Item: await IndexManyAsync<Item>(documents); break;
                default: throw new NotImplementedException();
            }
        }

        public async Task InsertAsync(dynamic document, IndexType indexType)
        {
            var documents = new[] { document };

            await InsertAsync(documents, indexType);
        }

        private async Task IndexManyAsync<TDocument>(IReadOnlyCollection<dynamic> documents) where TDocument : class =>
            await _elasticClient.IndexManyAsync(documents.Cast<TDocument>());
    }
}