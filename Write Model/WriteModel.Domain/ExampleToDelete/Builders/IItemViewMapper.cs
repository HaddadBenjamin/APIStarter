using WriteModel.Domain.ExampleToDelete.Aggregates;
using WriteModel.Domain.ExampleToDelete.Views;

namespace WriteModel.Domain.ExampleToDelete.Builders
{
    public interface IItemViewMapper
    {
        ItemView Map(Item item);
    }
}