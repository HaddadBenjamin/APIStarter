using System;

namespace APIStarter.Domain.CQRS.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(Guid id) : base($"Aggregate with id {id} not found") { }
    }
}