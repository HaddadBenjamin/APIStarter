using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIStarter.Domain.CQRS.Interfaces
{
    public interface IMediator
    {
        Task SendCommandAsync(ICommand command);
        Task<TQueryResult> SendQueryAsync<TQueryResult>(IQuery<TQueryResult> query);
        Task PublishEventsAsync(IReadOnlyCollection<IEvent> events);
    }
}