namespace ReadModel.ElasticSearch.Domain
{
    public interface IIndexName
    {
        string GetIndexName(IndexType indexType);
    }
}