using System.Threading.Tasks;

namespace ReadModel.Domain.Aliases
{
    public interface IAliasAdder
    {
        Task AddAsync(IndexType indexType, string indexName);
    }
}