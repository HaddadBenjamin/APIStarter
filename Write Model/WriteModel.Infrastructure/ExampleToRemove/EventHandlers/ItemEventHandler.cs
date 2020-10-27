using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WriteModel.Domain.ExampleToDelete.Events;
using WriteModel.Domain.ReadModel;
using WriteModel.Domain.ReadModel.Apis;

namespace WriteModel.Infrastructure.ExampleToRemove.EventHandlers
{
    public class ItemEventHandler : INotificationHandler<ItemWriteEvent>
    {
        private readonly IReadModelApi _readModelApi;

        public ItemEventHandler(IReadModelApi readModelApi) => _readModelApi = readModelApi;

        public async Task Handle(ItemWriteEvent @event, CancellationToken cancellationToken) =>
            await _readModelApi.RefreshDocumentAsync(IndexType.Item, @event.ItemId);
    }
}
