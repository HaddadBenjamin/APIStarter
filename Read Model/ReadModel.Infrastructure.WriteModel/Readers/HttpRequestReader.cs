using System;
using System.Collections.Generic;
using ReadModel.Domain.WriteModel.Readers;
using ReadModel.Domain.WriteModel.Views;
using ReadModel.Infrastructure.WriteModel.Clients;

namespace ReadModel.Infrastructure.WriteModel.Readers
{
    public class HttpRequestReader : IHttpRequestReader
    {
        private readonly WriteModelClient _client;

        public HttpRequestReader(WriteModelClient client) => _client = client;

        public IReadOnlyCollection<HttpRequestView> GetAll()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<HttpRequestView> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}