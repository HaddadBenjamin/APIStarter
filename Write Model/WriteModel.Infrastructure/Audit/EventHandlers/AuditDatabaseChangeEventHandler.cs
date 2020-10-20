using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WriteModel.Domain.Audit.Events;
using WriteModel.Domain.ReadModel;
using WriteModel.Domain.ReadModel.Apis;

namespace WriteModel.Infrastructure.Audit.EventHandlers
{
    public class AuditDatabaseChangeEventHandler : INotificationHandler<AuditDatabaseChangeWriteEvent>
    {
        private readonly IReadModelApi _readModelApi;

        public AuditDatabaseChangeEventHandler(IReadModelApi readModelApi) => _readModelApi = readModelApi;

        public async Task Handle(AuditDatabaseChangeWriteEvent @event, CancellationToken cancellationToken) =>
            await _readModelApi.RefreshDocumentAsync(IndexType.HttpRequest, @event.Id);
    }
}
