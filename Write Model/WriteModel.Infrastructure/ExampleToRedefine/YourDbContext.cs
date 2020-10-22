using Microsoft.EntityFrameworkCore;
using WriteModel.Domain.ExampleToDelete.Aggregates;
using WriteModel.Infrastructure.ExampleToRemove.DbContext;

namespace WriteModel.Infrastructure.ExampleToRedefine
{
    public class YourDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public YourDbContext(DbContextOptions<YourDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ItemMapper().Map(modelBuilder);
            new ItemLocationMapper().Map(modelBuilder);
        }
    }
}
