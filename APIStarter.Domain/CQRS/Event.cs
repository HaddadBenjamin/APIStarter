using System;
using APIStarter.Domain.Audit.Attributes;
using APIStarter.Domain.CQRS.Interfaces;

namespace APIStarter.Domain.CQRS
{
    public class Event : IEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [ShallNotAuditEvent] public Guid CorrelationId { get; set; }
        [ShallNotAuditEvent] public int Version { get; set; }
        [ShallNotAuditEvent] public DateTime Date { get; set; }
    }
}