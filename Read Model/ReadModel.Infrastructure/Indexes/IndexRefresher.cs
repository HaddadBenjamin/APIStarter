using System;
using System.Linq;
using System.Threading.Tasks;
using ReadModel.Domain;
using ReadModel.Domain.Indexes;
using ReadModel.Domain.Readers;

namespace ReadModel.Infrastructure.Indexes
{
    public class IndexRefresher : IIndexRefresher
    {
        private readonly IIndexCleaner _indexCleaner;
        private readonly IWriteModelReader _writeModelReader;

        public IndexRefresher(IIndexCleaner indexCleaner, IWriteModelReader writeModelReader)
        {
            _indexCleaner = indexCleaner;
            _writeModelReader = writeModelReader;
        }

        public async Task RefreshAllIndexesAsync()
        {
            var refreshIndexesTasks = ((IndexType[])Enum.GetValues(typeof(IndexType))).Select(RefreshIndexAsync).ToArray();

            Task.WaitAll(refreshIndexesTasks);
        }

        public async Task RefreshIndexAsync(IndexType indexType)
        {
            await _indexCleaner.CleanIndexAsync(indexType);

            var views = await _writeModelReader.GetAll(indexType);
            // map views to indexes
            // update data from index

            throw new NotImplementedException();
        }

        public async Task RefreshDocumentAsync(IndexType indexType, Guid id)
        {
            await _indexCleaner.CleanIndexAsync(indexType, id);

            var view = await _writeModelReader.GetById(indexType, id);
            // map view to index
            // update data from index

            throw new NotImplementedException();
        }
    }
}