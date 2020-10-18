using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReadModel.Domain;
using ReadModel.Domain.Readers;
using ReadModel.Domain.WriteModel.Readers;

namespace ReadModel.Infrastructure.Readers
{
    public class WriteModelReader : IWriteModelReader
    {
        private readonly IItemReader _itemReader;
        private readonly IHttpRequestReader _httpRequestReader;

        public WriteModelReader(IItemReader itemReader, IHttpRequestReader httpRequestReader)
        {
            _itemReader = itemReader;
            _httpRequestReader = httpRequestReader;
        }

        public async Task<IReadOnlyCollection<dynamic>> GetAll(IndexType indexType)
        {
            switch (indexType)
            {
                case IndexType.HttpRequest: await _httpRequestReader.GetAll(); break;
                case IndexType.Item: await _itemReader.GetAll(); break;
                default: throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }

        public async Task<dynamic> GetById(IndexType indexType, Guid id)
        {
            switch (indexType)
            {
                case IndexType.HttpRequest: await _httpRequestReader.GetById(id); break;
                case IndexType.Item: await _itemReader.GetById(id); break;
                default: throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }
    }
}
