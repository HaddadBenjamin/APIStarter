﻿using System;
using APIStarter.Domain.Audit.Commands;
using APIStarter.Domain.Audit.Services;
using APIStarter.Domain.AuthentificationContext;

namespace APIStarter.Domain.Audit.Aggregates
{
    /// <summary>
    /// Permet d'auditer toutes les requêtes faite à votre API.
    /// </summary>
    public class AuditRequest
    {
        public Guid Id { get; set; }
        public string HttpMethod { get; set; }
        public string Uri { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public int HttpStatus { get; set; }
        public string ResponseBody { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid CorrelationId { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid ImpersonatedUserId { get; set; }

        public static AuditRequest Create(CreateAuditRequest command, IAuthentificationContext authentificationContext, IAuditSerializer auditSerializer) => new AuditRequest
        {
            Id = Guid.NewGuid(),
            HttpMethod = command.Method,
            Uri = command.Uri,
            RequestHeaders = auditSerializer.Serialize(command.Headers),
            RequestBody = command.Body,
            HttpStatus = command.Status,
            ResponseBody = command.Message,
            Duration = command.Duration,
            CorrelationId = authentificationContext.CorrelationId,
            Date = DateTime.UtcNow,
            ImpersonatedUserId = authentificationContext.ImpersonatedUser.Id,
            UserId = authentificationContext.User.Id
        };
    }
}