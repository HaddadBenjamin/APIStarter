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
        private readonly IViewToDocumentMapper _viewToDocumentMapper;
        private readonly IDocumentInserter _documentInserter;

        public IndexRefresher(IIndexCleaner indexCleaner, IWriteModelReader writeModelReader, IViewToDocumentMapper viewToDocumentMapper, IDocumentInserter documentInserter)
        {
            _indexCleaner = indexCleaner;
            _writeModelReader = writeModelReader;
            _viewToDocumentMapper = viewToDocumentMapper;
            _documentInserter = documentInserter;
        }

        public async Task RefreshAllIndexesAsync()
        {
            var refreshIndexesTasks = ((IndexType[])Enum.GetValues(typeof(IndexType))).Select(RefreshIndexAsync).ToArray();

            await Task.WhenAll(refreshIndexesTasks);
        }

        public async Task RefreshIndexAsync(IndexType indexType)
        {
            var views = await _writeModelReader.GetAllAsync(indexType);
            var documents = _viewToDocumentMapper.Map(views, indexType);

            if (documents.Any())
                await _documentInserter.InsertAsync(documents, indexType);
        }

        public async Task RefreshDocumentAsync(IndexType indexType, Guid id)
        {
            await _indexCleaner.CleanIndexAsync(indexType, id);

            var view = await _writeModelReader.GetByIdAsync(indexType, id);

            if (view != null)
            {
                var document = _viewToDocumentMapper.Map(view, indexType);

                await _documentInserter.InsertAsync(document, indexType);
            }
        }
    }
}