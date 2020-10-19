using System.Threading;
using System.Threading.Tasks;
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
    public class AuditCommandHandler : IRequestHandler<CreateAuditCommand>
    {
        private readonly IAuthentificationContext _authentificationContext;
        private readonly AuditDbContext _dbContext;
        private readonly IAuditSerializer _auditSerializer;
        private readonly AuditConfiguration _auditConfiguration;

        public AuditCommandHandler(IAuthentificationContext authentificationContext, AuditDbContext dbContext, IAuditSerializer auditSerializer, AuditConfiguration auditConfiguration)
        {
            _authentificationContext = authentificationContext;
            _dbContext = dbContext;
            _auditSerializer = auditSerializer;
            _auditConfiguration = auditConfiguration;
        }

        public async Task<Unit> Handle(CreateAuditCommand command, CancellationToken cancellationToken)
        {
            if (!(_auditConfiguration.AuditCommands && command.Command.GetType().ShouldAuditCommand() && _authentificationContext.IsValid()))
                return Unit.Value;

            var auditCommand = AuditCommand.Create(command, _authentificationContext, _auditSerializer);

            _dbContext.AuditCommands.Add(auditCommand);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}