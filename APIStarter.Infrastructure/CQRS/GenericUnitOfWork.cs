using APIStarter.Domain.Audit.Services;
using APIStarter.Infrastructure.DbContext;

namespace APIStarter.Infrastructure.CQRS
{
    public class GenericUnitOfWork : UnitOfWork<YourDbContext>
    {
        public GenericUnitOfWork(YourDbContext dbContext, IDatabaseChangesAuditService databaseChangesAuditService) : base(dbContext, databaseChangesAuditService) { }
    }
}