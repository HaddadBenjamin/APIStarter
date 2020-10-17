using APIStarter.Domain.CQRS;
using APIStarter.Domain.CQRS.Interfaces;
using APIStarter.Infrastructure.DbContext;

namespace APIStarter.Infrastructure.CQRS
{
    public class GenericRepository<TAggregate> : Repository<TAggregate, YourDbContext> where TAggregate : AggregateRoot
    {
        public GenericRepository(YourDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork) { }
    }
}