using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using MediatR;
using WriteModel.Domain.Audit.Aggregates;
using WriteModel.Domain.Audit.Attributes;
using WriteModel.Domain.Audit.Commands;
using WriteModel.Domain.Audit.Configuration;
using WriteModel.Domain.Audit.Services;
using WriteModel.Domain.AuthentificationContext;
using WriteModel.Infrastructure.Audit.DbContext;

namespace WriteModel.Infrastructure.Audit.Handlers
{
    public class AuditEventHandler : IRequestHandler<CreateAuditEvents>
    {
        private readonly IAuthentificationContext _authentificationContext;
        private readonly AuditDbContext _dbContext;
        private readonly IAuditSerializer _auditSerializer;
        private readonly AuditConfiguration _auditConfiguration;

        public AuditEventHandler(IAuthentificationContext authentificationContext, AuditDbContext auditDbContext, IAuditSerializer auditSerializer, AuditConfiguration auditConfiguration)
        {
            _authentificationContext = authentificationContext;
            _dbContext = auditDbContext;
            _auditSerializer = auditSerializer;
            _auditConfiguration = auditConfiguration;
        }

        public async Task<Unit> Handle(CreateAuditEvents command, CancellationToken cancellationToken)
        {
            command.Events = command.Events.Where(@event => @event.GetType().ShouldAuditEvent()).ToList();

            if (!(_auditConfiguration.AuditEvents && _authentificationContext.IsValid() && command.Events.Any()))
                return Unit.Value;

            var auditEvents = AuditEvent.Create(command, _authentificationContext, _auditSerializer);

            await _dbContext.BulkInsertAsync(auditEvents);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}