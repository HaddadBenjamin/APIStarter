using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WriteModel.Domain.CQRS.Interfaces;
using WriteModel.Domain.ExampleToDelete.Aggregates;
using WriteModel.Domain.ExampleToDelete.Builders;
using WriteModel.Domain.ExampleToDelete.Queries;
using WriteModel.Domain.ExampleToDelete.Repositories;
using WriteModel.Domain.ExampleToDelete.Views;

namespace WriteModel.Infrastructure.ExampleToRemove.QueryHandlers
{
    public class ItemQueryHandler :
        IRequestHandler<GetItem, ItemView>,
        IRequestHandler<GetItemByName, ItemView>
    {
        private readonly ISession<Item, IItemRepository> _session;
        private readonly IItemViewMapper _itemViewMapper;

        public ItemQueryHandler(ISession<Item, IItemRepository> session, IItemViewMapper itemViewMapper)
        {
            _session = session;
            _itemViewMapper = itemViewMapper;
        }

        public async Task<ItemView> Handle(GetItem query, CancellationToken cancellationToken)
        {
            var item = _session.GetActive(query.Id, _ => _.Locations);

            return _itemViewMapper.Map(item);
        }

        public async Task<ItemView> Handle(GetItemByName query, CancellationToken cancellationToken)
        {
            var item = _session.Repository.GetByName(query.Name);

            return _itemViewMapper.Map(item);
        }
    }
}