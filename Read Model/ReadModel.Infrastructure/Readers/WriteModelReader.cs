using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReadModel.Domain;
using ReadModel.Domain.Readers;
using ReadModel.Domain.WriteModel.Readers;
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

        public async Task<IReadOnlyCollection<dynamic>> GetAllAsync(IndexType indexType) => await GetReader(indexType).GetAllAsync();

        public async Task<dynamic> GetByIdAsync(IndexType indexType, Guid id) => await GetReader(indexType).GetByIdAsync(id);

        private IWriteModelReader<dynamic> GetReader(IndexType indexType) => (IWriteModelReader<dynamic>)_writeModelReaders[indexType];
    }
}
