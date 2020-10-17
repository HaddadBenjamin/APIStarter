using APIStarter.Domain.ExampleToDelete.Aggregates;
using APIStarter.Domain.ExampleToDelete.Views;

namespace APIStarter.Domain.ExampleToDelete.Builders
{
    public interface IItemViewMapper
    {
        ItemView Map(Item item);
    }
}