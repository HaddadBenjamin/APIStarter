using System.Threading;
using System.Threading.Tasks;
using APIStarter.Domain.CQRS.Interfaces;
using APIStarter.Domain.ExampleToDelete.Aggregates;
using APIStarter.Domain.ExampleToDelete.Builders;
using APIStarter.Domain.ExampleToDelete.Queries;
using APIStarter.Domain.ExampleToDelete.Repositories;
using APIStarter.Domain.ExampleToDelete.Views;
using MediatR;

namespace APIStarter.Infrastructure.ExampleToRemove.QueryHandlers
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