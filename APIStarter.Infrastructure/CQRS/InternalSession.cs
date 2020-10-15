using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using APIStarter.Domain.AuthentificationContext;
using APIStarter.Domain.CQRS;
using APIStarter.Domain.CQRS.Exceptions;
using APIStarter.Domain.CQRS.Interfaces;
using APIStarter.Domain.Exceptions;

namespace APIStarter.Infrastructure.CQRS
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
        public TAggregate GetActive(Guid id) => InternalGet(() =>
        {
            var aggregate = Repository.GetActive<TAggregate>(id);

            if (aggregate is null && Repository.Get<TAggregate>(id) != null)
                throw new GoneException(id);

            return aggregate;
        }, id);
        public TAggregate GetActive<TPropertyIncluded>(Guid id, params Expression<Func<TAggregate, IEnumerable<TPropertyIncluded>>>[] includes) => InternalGet(() =>
        {
            var aggregate = Repository.GetActive(id, includes);

            if (aggregate is null && Repository.Get(id, includes) != null)
                throw new GoneException(id);

            return aggregate;
        }, id);

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

        public void Remove(TAggregate aggregate)
        {
            if (aggregate is null)
                throw new ArgumentNullException(nameof(aggregate));

            if (!_trackedAggregates.ContainsKey(aggregate.Id))
                throw new AggregateNotFoundException(aggregate.Id);

            Repository.Remove(aggregate);

            Track(aggregate);
        }

        public async Task<IReadOnlyCollection<IEvent>> SaveChanges()
        {
            var aggregates = _trackedAggregates.Values.ToList();
            var events = aggregates.SelectMany(a => a.FlushEvents()).ToList();

            foreach (var aggregate in aggregates)
                aggregate.MarkAsUpdated(_authentificationContext);

            foreach (var @event in events)
                @event.CorrelationId = _authentificationContext.CorrelationId;

            await _mediator.PublishEvents(events);
            await Repository.UnitOfWork.SaveChangesAsync();

            _trackedAggregates.Clear();

            return events;
        }
    }
}