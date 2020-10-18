using WriteModel.Domain.CQRS.Interfaces;
using WriteModel.Domain.ExampleToDelete.Aggregates;

namespace WriteModel.Domain.ExampleToDelete.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        Item GetByName(string name);
    }
}