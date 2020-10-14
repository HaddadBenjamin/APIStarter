using System.Threading;
using System.Threading.Tasks;
using APIStarter.Domain.CQRS.Interfaces;
using APIStarter.Domain.ExampleToDelete.Aggregates;
using APIStarter.Domain.ExampleToDelete.Commands;
using MediatR;

namespace APIStarter.Infrastructure.ExampleToRemove.CommandHandlers
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
            await _session.SaveChanges();

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateItem command, CancellationToken cancellationToken)
        {
            var aggregate = _session.Get(command.Id, _ => _.Locations);

            aggregate.Update(command);

            await _session.SaveChanges();

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteItem command, CancellationToken cancellationToken)
        {
            var aggregate = _session.Get(command.Id, _ => _.Locations);

            aggregate.Deactivate(command);

            await _session.SaveChanges();

            return Unit.Value;
        }
    }
}
