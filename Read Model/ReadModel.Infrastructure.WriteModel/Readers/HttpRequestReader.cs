using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReadModel.Domain.WriteModel.Readers;
using ReadModel.Domain.WriteModel.Views;
using ReadModel.Infrastructure.WriteModel.Clients;

namespace ReadModel.Infrastructure.WriteModel.Readers
{
    public class HttpRequestReader : IHttpRequestReader
    {
        private readonly WriteModelClient _client;

        public HttpRequestReader(WriteModelClient client) => _client = client;

        public async Task<IReadOnlyCollection<HttpRequestView>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<HttpRequestView> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}