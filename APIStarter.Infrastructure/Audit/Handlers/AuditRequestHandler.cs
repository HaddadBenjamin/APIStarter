using System.Threading;
using System.Threading.Tasks;
using APIStarter.Domain.Audit.Aggregates;
using APIStarter.Domain.Audit.Commands;
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

        public AuditRequestHandler(IAuthentificationContext authentificationContext, AuditDbContext dbContext, IAuditSerializer auditSerializer)
        {
            _authentificationContext = authentificationContext;
            _dbContext = dbContext;
            _auditSerializer = auditSerializer;
        }

        public async Task<Unit> Handle(CreateAuditRequest command, CancellationToken cancellationToken)
        {
            var auditRequest = AuditRequest.Create(command, _authentificationContext, _auditSerializer);

            _dbContext.AuditRequests.Add(auditRequest);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}