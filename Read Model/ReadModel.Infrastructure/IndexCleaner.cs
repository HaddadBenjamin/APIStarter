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
                case IndexType.HttpRequest: await CleanIndexAsync<HttpRequest>(); break;
                case IndexType.Item: await CleanIndexAsync<Item>(); break;
                default: throw new NotImplementedException();
            }
        }

        private async Task<DeleteByQueryResponse> CleanIndexAsync<TIndex>() where TIndex : class =>
            await _client.DeleteByQueryAsync<TIndex>(deleteByQueryDescriptor =>
                deleteByQueryDescriptor.Query(queryContainerDescriptor =>
                    queryContainerDescriptor.QueryString(queryStringQueryDescriptor =>
                        queryStringQueryDescriptor.Query("*"))));
    }
}