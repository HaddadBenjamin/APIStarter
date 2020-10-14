using APIStarter.Domain.CQRS.Interfaces;
using APIStarter.Domain.ExampleToDelete.Aggregates;

namespace APIStarter.Domain.ExampleToDelete.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        Item GetByName(string name);
    }
}