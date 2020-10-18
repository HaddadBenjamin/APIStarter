using System.Threading.Tasks;

namespace ReadModel.Domain.Interfaces
{
    public interface IIndexCleaner
    {
        Task CleanIndexAsync(IndexType indexType);
    }
}