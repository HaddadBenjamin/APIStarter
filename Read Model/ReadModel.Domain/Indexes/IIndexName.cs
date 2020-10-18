namespace ReadModel.Domain.Indexes
{
    public interface IIndexName
    {
        string GetIndexName(IndexType indexType);
    }
}