using System;
using System.Collections.Generic;
using ReadModel.Domain.WriteModel.Readers;
using ReadModel.Domain.WriteModel.Views;
using ReadModel.Infrastructure.WriteModel.Clients;

namespace ReadModel.Infrastructure.WriteModel.Readers
{
    public class ItemReader : IItemReader
    {
        private readonly WriteModelClient _client;

        public ItemReader(WriteModelClient client) => _client = client;

        public IReadOnlyCollection<ItemView> GetAll()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<ItemView> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
