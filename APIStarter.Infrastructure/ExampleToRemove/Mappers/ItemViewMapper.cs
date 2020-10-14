using System.Linq;
using APIStarter.Domain.ExampleToDelete.Aggregates;
using APIStarter.Domain.ExampleToDelete.Builders;
using APIStarter.Domain.ExampleToDelete.Views;

namespace APIStarter.Infrastructure.ExampleToRemove.Mappers
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
