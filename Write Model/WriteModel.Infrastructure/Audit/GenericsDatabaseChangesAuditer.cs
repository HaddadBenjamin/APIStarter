using WriteModel.Domain.Audit.Configuration;
using WriteModel.Domain.Audit.Services;
using WriteModel.Domain.AuthentificationContext;
using WriteModel.Infrastructure.Audit.DbContext;
using WriteModel.Infrastructure.Audit.Services;
using WriteModel.Infrastructure.DbContext;

namespace WriteModel.Infrastructure.Audit
{
    public class GenericsDatabaseChangesAuditService : DatabaseChangesAuditService<YourDbContext>
    {
        public GenericsDatabaseChangesAuditService(
            IAuthentificationContext authentificationContext,
            IAuditSerializer auditSerializer,
            AuditConfiguration auditConfiguration,
            AuditDbContext auditDbContext,
            YourDbContext dbContext) :
            base(authentificationContext, auditSerializer, auditConfiguration, auditDbContext, dbContext)
        { }
    }
}