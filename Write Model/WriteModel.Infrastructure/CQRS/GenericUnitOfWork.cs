using WriteModel.Domain.Audit.Services;
using WriteModel.Infrastructure.ExampleToRedefine;

namespace WriteModel.Infrastructure.CQRS
{
    public class GenericUnitOfWork : UnitOfWork<YourDbContext>
    {
        public GenericUnitOfWork(YourDbContext dbContext, IDatabaseChangesAuditService databaseChangesAuditService) : base(dbContext, databaseChangesAuditService) { }
    }
}