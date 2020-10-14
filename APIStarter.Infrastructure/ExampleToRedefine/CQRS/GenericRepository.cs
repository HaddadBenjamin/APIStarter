using APIStarter.Domain.CQRS;
using APIStarter.Domain.CQRS.Interfaces;
using APIStarter.Infrastructure.CQRS;
using APIStarter.Infrastructure.ExampleToRedefine.DbContext;

namespace APIStarter.Infrastructure.ExampleToRedefine.CQRS
{
    public class GenericRepository<TAggregate> : Repository<TAggregate, YourDbContext> where TAggregate : AggregateRoot
    {
        public GenericRepository(YourDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork) { }
    }
}