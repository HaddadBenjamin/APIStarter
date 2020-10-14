using APIStarter.Domain.Audit.Configuration;
using APIStarter.Domain.Audit.Services;
using APIStarter.Domain.AuthentificationContext;
using APIStarter.Infrastructure.Audit.DbContext;
using APIStarter.Infrastructure.Audit.Services;
using APIStarter.Infrastructure.ExampleToRedefine.DbContext;

namespace APIStarter.Infrastructure.ExampleToRedefine.Audit
{
    public class GenericsDatabaseChangesAuditService : DatabaseChangesAuditService<YourDbContext>
    {
        public GenericsDatabaseChangesAuditService(
            IAuthentificationContext authentificationContext,
            IAuditSerializer auditSerializer,
            AuditConfiguration auditConfiguration,
            AuditDbContext auditDbContext,
            YourDbContext dbContext) :
            base(authentificationContext, auditSerializer, auditConfiguration, auditDbContext, dbContext) { }
    }
}