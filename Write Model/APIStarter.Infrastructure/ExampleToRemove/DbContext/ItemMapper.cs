using APIStarter.Domain.ExampleToDelete.Aggregates;
using APIStarter.Infrastructure.DbContext.Mappers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIStarter.Infrastructure.ExampleToRemove.DbContext
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