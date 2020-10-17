using System.Linq;
using APIStarter.Domain.CQRS.Interfaces;
using APIStarter.Domain.ExampleToDelete.Aggregates;
using APIStarter.Domain.ExampleToDelete.Repositories;
using APIStarter.Infrastructure.CQRS;
using APIStarter.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace APIStarter.Infrastructure.ExampleToRemove.Repositories
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(YourDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public Item GetByName(string name) => Queryable
            .Include(i => i.Locations)
            .FirstOrDefault(i => i.Name == name && i.IsActive);
    }
}
