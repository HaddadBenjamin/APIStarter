using ReadModel.Domain;
using ReadModel.Domain.Aliases;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Indexes
{
    public class IndexNameWithAlias : IIndexNameWithAlias
    {
        private readonly IAliasContainsWithoutIndex _aliasContainsWithoutIndex;

        public IndexNameWithAlias(IAliasContainsWithoutIndex aliasContainsWithoutIndex) => _aliasContainsWithoutIndex = aliasContainsWithoutIndex;

        public string IndexName(IndexType indexType) => _aliasContainsWithoutIndex.Contains(indexType) ?
            IndexNameWithoutAlias.IndexName(indexType) :
            IndexNameWithoutAlias.TemporaryIndexName(indexType);

        public string TemporaryIndexName(IndexType indexType) => _aliasContainsWithoutIndex.Contains(indexType) ?
            IndexNameWithoutAlias.TemporaryIndexName(indexType) :
            IndexNameWithoutAlias.IndexName(indexType);
    }
}