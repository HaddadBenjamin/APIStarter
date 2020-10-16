using System.Threading;
using System.Threading.Tasks;
using APIStarter.Domain.Audit.Aggregates;
using APIStarter.Domain.Audit.Attributes;
using APIStarter.Domain.Audit.Commands;
using APIStarter.Domain.Audit.Configuration;
using APIStarter.Domain.Audit.Services;
using APIStarter.Domain.AuthentificationContext;
using APIStarter.Infrastructure.Audit.DbContext;
using MediatR;

namespace APIStarter.Infrastructure.Audit.Handlers
{
    public class AuditQueryHandler : IRequestHandler<CreateAuditQuery>
    {
        private readonly IAuthentificationContext _authentificationContext;
        private readonly AuditDbContext _dbContext;
        private readonly IAuditSerializer _auditSerializer;
        private readonly AuditConfiguration _auditConfiguration;

        public AuditQueryHandler(IAuthentificationContext authentificationContext, AuditDbContext dbContext, IAuditSerializer auditSerializer, AuditConfiguration auditConfiguration)
        {
            _authentificationContext = authentificationContext;
            _dbContext = dbContext;
            _auditSerializer = auditSerializer;
            _auditConfiguration = auditConfiguration;
        }

        public async Task<Unit> Handle(CreateAuditQuery query, CancellationToken cancellationToken)
        {
            if (!(_auditConfiguration.AuditQueries && query.Query.GetType().ShouldAuditQuery() && _authentificationContext.IsValid()))
                return Unit.Value;

            var auditQuery = AuditQuery.Create(query, _authentificationContext, _auditSerializer);

            _dbContext.AuditQueries.Add(auditQuery);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}