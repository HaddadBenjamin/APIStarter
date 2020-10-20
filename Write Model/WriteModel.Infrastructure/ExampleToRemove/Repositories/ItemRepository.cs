using System.Linq;
using Microsoft.EntityFrameworkCore;
using WriteModel.Domain.CQRS.Interfaces;
using WriteModel.Domain.ExampleToDelete.Aggregates;
using WriteModel.Domain.ExampleToDelete.Repositories;
using WriteModel.Infrastructure.CQRS;
using WriteModel.Infrastructure.ExampleToRedefine;

namespace WriteModel.Infrastructure.ExampleToRemove.Repositories
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(YourDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public Item GetByName(string name) => Queryable
            .Include(i => i.Locations)
            .FirstOrDefault(i => i.Name == name && i.IsActive);
    }
}
