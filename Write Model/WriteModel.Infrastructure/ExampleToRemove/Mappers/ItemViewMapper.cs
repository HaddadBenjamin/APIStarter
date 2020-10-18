using System.Linq;
using WriteModel.Domain.ExampleToDelete.Aggregates;
using WriteModel.Domain.ExampleToDelete.Builders;
using WriteModel.Domain.ExampleToDelete.Views;

namespace WriteModel.Infrastructure.ExampleToRemove.Mappers
{
    public class ItemViewMapper : IItemViewMapper
    {
        public ItemView Map(Item item) => new ItemView
        {
            Id = item.Id,
            Name = item.Name,
            Locations = item.Locations.Select(l => new ItemLocationView
            {
                Id = l.Id,
                Name = l.Name
            })
        };
    }
}
