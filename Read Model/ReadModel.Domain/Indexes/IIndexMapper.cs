using Nest;

namespace ReadModel.Domain.Indexes
{
    public interface IIndexMapper
    {
        CreateIndexDescriptor Map(IndexType indexType, CreateIndexDescriptor createIndexDescriptor);
    }
}