using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using APIStarter.Domain.Audit.Aggregates;
using APIStarter.Domain.Audit.Attributes;
using APIStarter.Domain.Audit.Commands;
using APIStarter.Domain.Audit.Configuration;
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