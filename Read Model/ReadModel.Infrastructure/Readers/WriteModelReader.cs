using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReadModel.Domain;
using ReadModel.Domain.Readers;
using ReadModel.Infrastructure.WriteModel.Readers;

namespace ReadModel.Infrastructure.Readers
{
    public class WriteModelReader : IWriteModelReader
    {
        private readonly ItemReader _itemReader;
        private readonly HttpRequestReader _httpRequestReader;

        public WriteModelReader(ItemReader itemReader, HttpRequestReader httpRequestReader)
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
