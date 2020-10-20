using System;
using WriteModel.Domain.CQRS;

namespace WriteModel.Domain.Audit.Events
{
    public class AuditDatabaseChangeWriteEvent : Event
    {
        public Guid Id { get; }

        public AuditDatabaseChangeWriteEvent(Guid id) => Id = id;
    }
}
