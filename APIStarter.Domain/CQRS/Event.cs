using System;
using APIStarter.Domain.CQRS.Interfaces;

namespace APIStarter.Domain.CQRS
{
    public class Event : IEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CorrelationId { get; set; }
        public int Version { get; set; }
        public DateTime Date { get; set; }
    }
}