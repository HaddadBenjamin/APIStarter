namespace ReadModel.ElasticSearch.Domain.Interfaces
{
    public interface IIndexName
    {
        string GetIndexName(IndexType indexType);
    }
}