using WriteModel.Domain.CQRS;
using WriteModel.Domain.CQRS.Interfaces;
using WriteModel.Infrastructure.ExampleToRedefine;

namespace WriteModel.Infrastructure.CQRS
{
    public class GenericRepository<TAggregate> : Repository<TAggregate, YourDbContext> where TAggregate : AggregateRoot
    {
        public GenericRepository(YourDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork) { }
    }
}