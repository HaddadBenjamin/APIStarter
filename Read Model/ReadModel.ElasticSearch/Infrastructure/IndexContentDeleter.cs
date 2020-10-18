using System;
using System.Threading.Tasks;
using Nest;
using ReadModel.ElasticSearch.Domain;
using ReadModel.ElasticSearch.Domain.Interfaces;
using ReadModel.ElasticSearch.Domain.Models;

namespace ReadModel.ElasticSearch.Infrastructure
{
    public class IndexContentDeleter : IIndexContentDeleter
    {
        private readonly ElasticClient _client;

        public IndexContentDeleter(IReadModelClient readModelClient) => _client = readModelClient.ElasticClient;

        public async Task DeleteIndexAsync(IndexType indexType)
        {
            switch (indexType)
            {
                case IndexType.AuditRequest: await DeleteIndexAsync<AuditRequest>(); break;
                case IndexType.Item: await DeleteIndexAsync<Item>(); break;
                default: throw new NotImplementedException();
            }
        }

        private async Task<DeleteByQueryResponse> DeleteIndexAsync<TIndex>() where TIndex : class =>
            await _client.DeleteByQueryAsync<TIndex>(deleteByQueryDescriptor =>
                deleteByQueryDescriptor.Query(queryContainerDescriptor =>
                    queryContainerDescriptor.QueryString(queryStringQueryDescriptor =>
                        queryStringQueryDescriptor.Query("*"))));

    }
}