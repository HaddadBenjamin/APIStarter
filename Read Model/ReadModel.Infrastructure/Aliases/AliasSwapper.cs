using System;
using System.Linq;
using System.Threading.Tasks;
using ReadModel.Domain;
using ReadModel.Domain.Aliases;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Aliases
{
    public class AliasSwapper : IAliasSwapper
    {
        private readonly IIndexNameWithAlias _indexName;
        private readonly IAliasAdder _aliasAdder;
        private readonly IAliasRemoval _aliasRemoval;

        public AliasSwapper(IIndexNameWithAlias indexName, IAliasAdder aliasAdder, IAliasRemoval aliasRemoval)
        {
            _indexName = indexName;
            _aliasAdder = aliasAdder;
            _aliasRemoval = aliasRemoval;
        }

        public async Task SwapAllIndexesAsync()
        {
            var swapIndexesAliasesTasks = ((IndexType[])Enum.GetValues(typeof(IndexType))).Select(SwapIndexAsync).ToArray();

            await Task.WhenAll(swapIndexesAliasesTasks);
        }

        public async Task SwapIndexAsync(IndexType indexType)
        {
            var temporaryIndexName = _indexName.TemporaryIndexName(indexType);

            await _aliasRemoval.RemoveAsync(indexType);
            await _aliasAdder.AddAsync(indexType, temporaryIndexName);
        }
    }
}