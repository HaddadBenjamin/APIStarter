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
        private readonly Dictionary<IndexType, object> _writeModelReaders;

        public WriteModelReader(ItemReader itemReader, HttpRequestReader httpRequestReader) =>
            _writeModelReaders = new Dictionary<IndexType, object>
            {
                { IndexType.HttpRequest, httpRequestReader },
                { IndexType.Item, itemReader },
            };

        public async Task<IReadOnlyCollection<object>> GetAllAsync(IndexType indexType) => await GetReader(indexType).GetAllAsync();

        public async Task<object> GetByIdAsync(IndexType indexType, Guid id) => await GetReader(indexType).GetByIdAsync(id);

        private dynamic GetReader(IndexType indexType) => _writeModelReaders[indexType];
    }
}
