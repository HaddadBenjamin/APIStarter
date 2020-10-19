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

        public async Task<IReadOnlyCollection<dynamic>> GetAllAsync(IndexType indexType)
        {
            switch (indexType)
            {
                case IndexType.HttpRequest: return await _httpRequestReader.GetAllAsync();
                case IndexType.Item: return await _itemReader.GetAllAsync();
                default: throw new NotImplementedException();
            }
        }

        public async Task<dynamic> GetByIdAsync(IndexType indexType, Guid id)
        {
            switch (indexType)
            {
                case IndexType.HttpRequest: return await _httpRequestReader.GetByIdAsync(id);
                case IndexType.Item: return await _itemReader.GetByIdAsync(id);
                default: throw new NotImplementedException();
            }
        }
    }
}
