using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Clients;
using ReadModel.Domain.Index.HttpRequest;
using ReadModel.Domain.Index.Item;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Indexes
{
    public class IndexCleaner : IIndexCleaner
    {
        private readonly ElasticClient _client;
        private Dictionary<IndexType, Func<Guid?, Task<ResponseBase>>> _indexCleaners;

        public IndexCleaner(IReadModelClient readModelClient)
        {
            _client = readModelClient.ElasticClient;
            _indexCleaners = new Dictionary<IndexType, Func<Guid?, Task<ResponseBase>>>
            {
                { IndexType.Item, CleanIndexAsync<Item> },
                { IndexType.HttpRequest, CleanIndexAsync<HttpRequest> },
            };
        }

        public async Task CleanIndexAsync(IndexType indexType) => await _indexCleaners[indexType](null);
        public async Task CleanIndexAsync(IndexType indexType, Guid id) => await _indexCleaners[indexType](id);

        private async Task<ResponseBase> CleanIndexAsync<TIndex>(Guid? id) where TIndex : class =>
            id != null ? await _client.DeleteAsync<TIndex>(id) :
            (ResponseBase)await _client.DeleteByQueryAsync<TIndex>(deleteByQueryDescriptor =>
               deleteByQueryDescriptor.Query(queryContainerDescriptor =>
                   queryContainerDescriptor.QueryString(queryStringQueryDescriptor =>
                       queryStringQueryDescriptor.Query("*"))));
    }
}