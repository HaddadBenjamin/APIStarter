using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WriteModel.Domain.ExampleToDelete.Aggregates;
using WriteModel.Infrastructure.DbContext.Mappers;

namespace WriteModel.Infrastructure.ExampleToRemove.DbContext
{
    public class ItemMapper : AggregateRootMap<Item>
    {
        protected override void Map(EntityTypeBuilder<Item> entity)
        {
            entity.Property(item => item.Name).IsRequired();

            entity.HasMany(item => item.Locations)
                .WithOne()
                .HasForeignKey(location => location.ItemId);
        }
    }
}