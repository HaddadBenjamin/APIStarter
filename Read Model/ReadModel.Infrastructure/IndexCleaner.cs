using System;
using System.Threading.Tasks;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Interfaces;
using ReadModel.Domain.Models;

namespace ReadModel.Infrastructure
{
    public class IndexCleaner : IIndexCleaner
    {
        private readonly ElasticClient _client;

        public IndexCleaner(IReadModelClient readModelClient) => _client = readModelClient.ElasticClient;

        public async Task CleanIndexAsync(IndexType indexType)
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