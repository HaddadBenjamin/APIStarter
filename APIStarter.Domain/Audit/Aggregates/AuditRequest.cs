using System;
using APIStarter.Domain.Audit.Commands;
using APIStarter.Domain.Audit.Services;
using APIStarter.Domain.AuthentificationContext;

namespace APIStarter.Domain.Audit.Aggregates
{
    /// <summary>
    /// Permet d'auditer tous les requêtes faite à votre API.
    /// </summary>
    public class AuditRequest
    {
        public Guid Id { get; set; }
        public string Method { get; set; }
        public string Uri { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid CorrelationId { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid ImpersonatedUserId { get; set; }

        public static AuditRequest Create(CreateAuditRequest command, IAuthentificationContext authentificationContext, IAuditSerializer auditSerializer) => new AuditRequest
        {
            Id = Guid.NewGuid(),
            Method = command.Method,
            Uri = command.Uri,
            Headers = auditSerializer.Serialize(command.Headers),
            Body = auditSerializer.Serialize(command.Body),
            Status = command.Status,
            Message = command.Message,
            Duration = command.Duration,
            CorrelationId = authentificationContext.CorrelationId,
            Date = DateTime.UtcNow,
            ImpersonatedUserId = authentificationContext.ImpersonatedUser.Id,
            UserId = authentificationContext.User.Id
        };
    }
}