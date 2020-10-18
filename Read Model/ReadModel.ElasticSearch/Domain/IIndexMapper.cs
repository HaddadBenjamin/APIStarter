using Nest;

namespace ReadModel.ElasticSearch
{
    public interface IIndexMapper
    {
        CreateIndexDescriptor Map(IndexType indexType, CreateIndexDescriptor createIndexDescriptor);
    }
}