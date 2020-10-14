using APIStarter.Domain.ExampleToDelete.Aggregates;
using APIStarter.Infrastructure.DbContext.Mappers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIStarter.Infrastructure.ExampleToRemove.DbContext
{
    public class ItemLocationMapper : AggregateMap<ItemLocation>
    {
        protected override void Map(EntityTypeBuilder<ItemLocation> entity)
        {
            entity.HasKey(itemLocation => new { itemLocation.ItemId, itemLocation.Id });

            entity.HasIndex(itemLocation => itemLocation.Id);
            entity.HasIndex(itemLocation => itemLocation.ItemId);
            entity.HasIndex(itemLocation => new { itemLocation.Id, itemLocation.ItemId });

            entity.HasOne(ItemLocation => ItemLocation.Item)
                .WithMany(item => item.Locations)
                .HasForeignKey(itemLocation => itemLocation.ItemId)
                .IsRequired();
        }
    }
}