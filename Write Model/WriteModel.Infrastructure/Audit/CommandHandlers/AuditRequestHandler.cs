using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WriteModel.Domain.Audit.Aggregates;
using WriteModel.Domain.Audit.Commands;
using WriteModel.Domain.Audit.Configuration;
using WriteModel.Domain.Audit.Services;
using WriteModel.Domain.AuthentificationContext;
using WriteModel.Infrastructure.Audit.DbContext;

namespace WriteModel.Infrastructure.Audit.CommandHandlers
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
            if (!(_auditConfiguration.AuditRequests && _authentificationContext.IsValid()))
                return Unit.Value;

            var auditRequest = AuditRequest.Create(command, _authentificationContext, _auditSerializer);

            _dbContext.AuditRequests.Add(auditRequest);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}