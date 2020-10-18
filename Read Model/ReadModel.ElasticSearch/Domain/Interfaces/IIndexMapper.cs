using Nest;

namespace ReadModel.ElasticSearch.Domain.Interfaces
{
    public interface IIndexMapper
    {
        CreateIndexDescriptor Map(IndexType indexType, CreateIndexDescriptor createIndexDescriptor);
    }
}