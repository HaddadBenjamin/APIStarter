using System.Threading;
using System.Threading.Tasks;
using APIStarter.Domain.ExampleToDelete.Events;
using MediatR;

namespace APIStarter.Infrastructure.ExampleToRemove.EventHandlers
{
    public class ItemEventHandler : INotificationHandler<ItemWriteEvent>
    {
        public async Task Handle(ItemWriteEvent notification, CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}
