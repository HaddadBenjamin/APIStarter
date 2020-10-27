using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WriteModel.Domain.AuthentificationContext;
using WriteModel.Domain.CQRS;
using WriteModel.Domain.CQRS.Exceptions;
using WriteModel.Domain.CQRS.Interfaces;
using WriteModel.Domain.Tools.Exceptions;

namespace WriteModel.Infrastructure.CQRS
{
    public class InternalSession<TAggregate> : IInternalSession<TAggregate> where TAggregate : AggregateRoot
    {
        private readonly IAuthentificationContext _authentificationContext;
        private readonly IMediator _mediator;
        private readonly ConcurrentDictionary<Guid, AggregateRoot> _trackedAggregates = new ConcurrentDictionary<Guid, AggregateRoot>();

        public IRepository<TAggregate> Repository { get; }
        public IUnitOfWork UnitOfWork => Repository.UnitOfWork;
        public IQueryable<TAggregate> Queryable => Repository.Queryable;

        public InternalSession(IRepository<TAggregate> repository, IAuthentificationContext authentificationContext, IMediator mediator)
        {
            Repository = repository;
            _authentificationContext = authentificationContext;
            _mediator = mediator;
        }

        public void Track(TAggregate aggregate)
        {
            if (aggregate is null)
                throw new ArgumentNullException(nameof(aggregate));

            _trackedAggregates.AddOrUpdate(aggregate.Id, aggregate, (key, value) => aggregate);
        }

        public void UnTrack(TAggregate aggregate)
        {
            if (aggregate is null)
                throw new ArgumentNullException(nameof(aggregate));

            _trackedAggregates.TryRemove(aggregate.Id, out _);
        }

        private TAggregate InternalGet(Func<TAggregate> getAggregate, Guid id)
        {
            var aggregate = _trackedAggregates.OfType<TAggregate>().SingleOrDefault(t => t.Id == id);

            if (aggregate != null)
                return aggregate;

            aggregate = getAggregate();

            if (aggregate is null)
                throw new AggregateNotFoundException(id);

            Track(aggregate);

            return aggregate;
        }

        public TAggregate Get(Guid id) => InternalGet(() => Repository.Get<TAggregate>(id), id);
        public TAggregate Get<TPropertyIncluded>(Guid id, params Expression<Func<TAggregate, IEnumerable<TPropertyIncluded>>>[] includes) => InternalGet(() => Repository.Get(id, includes), id);
        public TAggregate GetActive(Guid id) => InternalGet(() => Repository.GetActive<TAggregate>(id), id);
        public TAggregate GetActive<TPropertyIncluded>(Guid id, params Expression<Func<TAggregate, IEnumerable<TPropertyIncluded>>>[] includes) => InternalGet(() => Repository.GetActive(id, includes), id);

        public IQueryable<TAggregate> Search() => Repository.Search();
        public IQueryable<TAggregate> Search<TPropertyIncluded>(params Expression<Func<TAggregate, IEnumerable<TPropertyIncluded>>>[] includes) => Repository.Search(includes);
        public IQueryable<TAggregate> SearchActive() => Repository.SearchActive();
        public IQueryable<TAggregate> SearchActive<TProperty>(params Expression<Func<TAggregate, IEnumerable<TProperty>>>[] includes) => Repository.SearchActive(includes);

        public void Add(TAggregate aggregate)
        {
            if (aggregate is null)
                throw new ArgumentNullException(nameof(aggregate));

            aggregate.MarkAsCreated(_authentificationContext);
            Repository.Add(aggregate);

            Track(aggregate);
        }

        public void Update(TAggregate aggregate)
        {
            if (aggregate is null)
                throw new ArgumentNullException(nameof(aggregate));

            if (!_trackedAggregates.ContainsKey(aggregate.Id))
                throw new AggregateNotFoundException(aggregate.Id);

            Repository.Update(aggregate);

            Track(aggregate);
        }

        public void Remove(TAggregate aggregate) => BaseRemove(aggregate, () => Repository.Remove((aggregate)));

        public void Deactivate(TAggregate aggregate) => BaseRemove(aggregate, () =>
        {
            if (!aggregate.IsActive)
                throw new GoneException(aggregate.Id);

            Repository.Deactivate(aggregate);
        });

        private void BaseRemove(TAggregate aggregate, Action removeAction)
        {
            if (aggregate is null)
                throw new ArgumentNullException(nameof(aggregate));

            if (!_trackedAggregates.ContainsKey(aggregate.Id))
                throw new AggregateNotFoundException(aggregate.Id);

            removeAction?.Invoke();

            Track(aggregate);
        }

        public async Task<IReadOnlyCollection<IEvent>> SaveChangesAsync()
        {
            var aggregates = _trackedAggregates.Values.ToList();
            var events = aggregates.SelectMany(a => a.FlushEvents()).ToList();

            foreach (var aggregate in aggregates)
                aggregate.MarkAsUpdated(_authentificationContext);

            foreach (var @event in events)
                @event.CorrelationId = _authentificationContext.CorrelationId;

            await Repository.UnitOfWork.SaveChangesAsync();
            await _mediator.PublishEventsAsync(events);

            _trackedAggregates.Clear();

            return events;
        }
    }
}