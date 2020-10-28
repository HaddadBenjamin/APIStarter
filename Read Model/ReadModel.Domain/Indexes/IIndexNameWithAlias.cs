namespace ReadModel.Domain.Indexes
{
    public interface IIndexNameWithAlias
    {
        string IndexName(IndexType indexType);
        string TemporaryIndexName(IndexType indexType);
    }
}