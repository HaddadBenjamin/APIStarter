using System;
using MediatR;
using WriteModel.Domain.Audit.Attributes;

namespace WriteModel.Domain.CQRS.Interfaces
{
    public interface IEvent : INotification
    {
        Guid Id { get; set; }
        [ShallNotAuditEvent] Guid CorrelationId { get; set; }
        [ShallNotAuditEvent] int Version { get; set; }
        [ShallNotAuditEvent] DateTime Date { get; set; }
    }
}