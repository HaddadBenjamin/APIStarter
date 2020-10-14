using System.Threading;
using System.Threading.Tasks;
using APIStarter.Domain.Audit.Aggregates;
using APIStarter.Domain.Audit.Commands;
using APIStarter.Domain.Audit.Services;
using APIStarter.Domain.AuthentificationContext;
using APIStarter.Infrastructure.Audit.DbContext;
using EFCore.BulkExtensions;
using MediatR;

namespace APIStarter.Infrastructure.Audit.Handlers
{
    public class AuditEventHandler : IRequestHandler<CreateAuditEvents>
    {
        private readonly IAuthentificationContext _authentificationContext;
        private readonly AuditDbContext _dbContext;
        private readonly IAuditSerializer _auditSerializer;

        public AuditEventHandler(IAuthentificationContext authentificationContext, AuditDbContext auditDbContext, IAuditSerializer auditSerializer)
        {
            _authentificationContext = authentificationContext;
            _dbContext = auditDbContext;
            _auditSerializer = auditSerializer;
        }

        public async Task<Unit> Handle(CreateAuditEvents command, CancellationToken cancellationToken)
        {
            var auditEvents = AuditEvent.Create(command, _authentificationContext, _auditSerializer);

            await _dbContext.BulkInsertAsync(auditEvents);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}