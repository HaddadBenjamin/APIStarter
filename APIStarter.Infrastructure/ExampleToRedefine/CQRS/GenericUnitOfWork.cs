using APIStarter.Domain.Audit.Services;
using APIStarter.Infrastructure.CQRS;
using APIStarter.Infrastructure.ExampleToRedefine.DbContext;

namespace APIStarter.Infrastructure.ExampleToRedefine.CQRS
{
    public class GenericUnitOfWork : UnitOfWork<YourDbContext>
    {
        public GenericUnitOfWork(YourDbContext dbContext, IDatabaseChangesAuditService databaseChangesAuditService) : base(dbContext, databaseChangesAuditService) { }
    }
}