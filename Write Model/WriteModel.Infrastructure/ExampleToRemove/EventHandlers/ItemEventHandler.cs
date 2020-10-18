using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WriteModel.Domain.ExampleToDelete.Events;

namespace WriteModel.Infrastructure.ExampleToRemove.EventHandlers
{
    public class ItemEventHandler : INotificationHandler<ItemWriteEvent>
    {
        public async Task Handle(ItemWriteEvent notification, CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}
