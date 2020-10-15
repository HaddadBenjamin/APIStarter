using System.Threading;
using System.Threading.Tasks;
using APIStarter.Domain.Audit.Aggregates;
using APIStarter.Domain.Audit.Commands;
using APIStarter.Domain.Audit.Configuration;
using APIStarter.Domain.Audit.Services;
using APIStarter.Domain.AuthentificationContext;
using APIStarter.Infrastructure.Audit.DbContext;
using MediatR;

namespace APIStarter.Infrastructure.Audit.Handlers
{
    public class AuditRequestHandler : IRequestHandler<CreateAuditRequest>
    {
        private readonly IAuthentificationContext _authentificationContext;
        private readonly AuditDbContext _dbContext;
        private readonly IAuditSerializer _auditSerializer;
        private readonly AuditConfiguration _auditConfiguration;

        public AuditRequestHandler(IAuthentificationContext authentificationContext, AuditDbContext dbContext, IAuditSerializer auditSerializer, AuditConfiguration auditConfiguration)
        {
            _authentificationContext = authentificationContext;
            _dbContext = dbContext;
            _auditSerializer = auditSerializer;
            _auditConfiguration = auditConfiguration;
        }

        public async Task<Unit> Handle(CreateAuditRequest command, CancellationToken cancellationToken)
        {
            if (!_auditConfiguration.AuditRequests)
                return Unit.Value;

            var auditRequest = AuditRequest.Create(command, _authentificationContext, _auditSerializer);

            _dbContext.AuditRequests.Add(auditRequest);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}