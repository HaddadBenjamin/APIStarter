using System;
using WriteModel.Domain.Audit.Attributes;
using WriteModel.Domain.CQRS.Interfaces;

namespace WriteModel.Domain.CQRS
{
    public class Event : IEvent
    {
        [ShallNotAuditEvent] public Guid Id { get; set; } = Guid.NewGuid();
        [ShallNotAuditEvent] public Guid CorrelationId { get; set; }
        [ShallNotAuditEvent] public int Version { get; set; }
        [ShallNotAuditEvent] public DateTime Date { get; set; }
    }
}