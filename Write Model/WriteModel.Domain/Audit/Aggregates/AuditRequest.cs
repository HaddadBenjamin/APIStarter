using System;
using WriteModel.Domain.Audit.Commands;
using WriteModel.Domain.Audit.Services;
using WriteModel.Domain.AuthentificationContext;

namespace WriteModel.Domain.Audit.Aggregates
{
    /// <summary>
    /// Permet d'auditer toutes les requêtes faite à votre API.
    /// </summary>
    public class AuditRequest
    {
        public Guid Id { get; set; }

        public string ClientApplication { get; set; }
        public string IPv4 { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string HttpMethod { get; set; }
        public string Uri { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public string UserAgent { get; set; }

        public string ResponseBody { get; set; }
        public int HttpStatus { get; set; }
        public TimeSpan Duration { get; set; }

        public Guid CorrelationId { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid ImpersonatedUserId { get; set; }

        public static AuditRequest Create(CreateAuditRequest command, IAuthentificationContext authentificationContext, IAuditSerializer auditSerializer) => new AuditRequest
        {
            Id = Guid.NewGuid(),

            ClientApplication = command.ClientApplication,
            IPv4 = command.IPv4,
            Longitude = command.Longitude,
            Latitude = command.Latitude,

            HttpMethod = command.HttpMethod,
            Uri = command.Uri,
            RequestHeaders = auditSerializer.Serialize(command.RequestHeaders),
            RequestBody = command.RequestBody,
            UserAgent = command.UserAgent,

            ResponseBody = command.ResponseBody,
            HttpStatus = command.HttpStatus,
            Duration = command.Duration,

            CorrelationId = authentificationContext.CorrelationId,
            Date = DateTime.UtcNow,
            ImpersonatedUserId = authentificationContext.ImpersonatedUser.Id,
            UserId = authentificationContext.User.Id
        };
    }
}