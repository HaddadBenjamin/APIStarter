using System;
using APIStarter.Domain.Audit.Attributes;
using MediatR;

namespace APIStarter.Domain.CQRS.Interfaces
{
    public interface IEvent : INotification
    {
        Guid Id { get; set; }
        [ShallNotAuditEvent] Guid CorrelationId { get; set; }
        [ShallNotAuditEvent] int Version { get; set; }
        [ShallNotAuditEvent] DateTime Date { get; set; }
    }
}