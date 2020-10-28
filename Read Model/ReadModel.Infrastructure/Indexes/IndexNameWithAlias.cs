using ReadModel.Domain;
using ReadModel.Domain.Aliases;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Indexes
{
    public class IndexNameWithAlias : IIndexNameWithAlias
    {
        private readonly IAliasContains _aliasContains;

        public IndexNameWithAlias(IAliasContains aliasContains) => _aliasContains = aliasContains;

        public string IndexName(IndexType indexType) => _aliasContains.Contains(indexType) ?
            IndexNameWithoutAlias.TemporaryIndexName(indexType) :
            IndexNameWithoutAlias.IndexName(indexType);

        public string TemporaryIndexName(IndexType indexType) => _aliasContains.Contains(indexType) ?
            IndexNameWithoutAlias.IndexName(indexType) :
            IndexNameWithoutAlias.TemporaryIndexName(indexType);

        public string AliasName(IndexType indexType) => IndexNameWithoutAlias.AliasName(indexType);
    }
}