using Nest;

namespace ReadModel.ElasticSearch.Domain
{
    public interface IIndexMapper
    {
        CreateIndexDescriptor Map(IndexType indexType, CreateIndexDescriptor createIndexDescriptor);
    }
}