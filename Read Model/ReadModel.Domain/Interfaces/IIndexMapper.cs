using Nest;

namespace ReadModel.Domain.Interfaces
{
    public interface IIndexMapper
    {
        CreateIndexDescriptor Map(IndexType indexType, CreateIndexDescriptor createIndexDescriptor);
    }
}