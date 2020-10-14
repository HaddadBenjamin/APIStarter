using System;
using MediatR;

namespace APIStarter.Domain.CQRS.Interfaces
{
    public interface IEvent : INotification
    {
        Guid Id { get; set; }
        Guid CorrelationId { get; set; }
        int Version { get; set; }
        DateTime Date { get; set; }
    }
}