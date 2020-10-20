using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WriteModel.Domain.CQRS.Interfaces;
using WriteModel.Domain.ExampleToDelete.Aggregates;
using WriteModel.Domain.ExampleToDelete.Commands;
using WriteModel.Domain.Tools.Exceptions;

namespace WriteModel.Infrastructure.ExampleToRemove.CommandHandlers
{
    public class ItemHandler :
        IRequestHandler<CreateItem>,
        IRequestHandler<UpdateItem>,
        IRequestHandler<DeleteItem>
    {
        private readonly ISession<Item> _session;

        public ItemHandler(ISession<Item> session) => _session = session;

        public async Task<Unit> Handle(CreateItem command, CancellationToken cancellationToken)
        {
            var aggregate = new Item().Create(command);

            _session.Add(aggregate);
            await _session.SaveChangesAsync();

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateItem command, CancellationToken cancellationToken)
        {
            var aggregate = _session.GetActive(command.Id, _ => _.Locations);

            aggregate.Update(command);

            await _session.SaveChangesAsync();

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteItem command, CancellationToken cancellationToken)
        {
            var aggregate = _session.Get(command.Id, _ => _.Locations);

            if (!aggregate.IsActive)
                throw new GoneException(command.Id);

            aggregate.Deactivate(command);

            await _session.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
