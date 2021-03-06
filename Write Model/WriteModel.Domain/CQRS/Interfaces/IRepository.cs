﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WriteModel.Domain.CQRS.Interfaces
{
    public interface IRepository<TAggregate> where TAggregate : AggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        IQueryable<TAggregate> Queryable { get; }

        TAggregate Get(Guid id);
        TAggregate Get<TPropertyIncluded>(Guid id, params Expression<Func<TAggregate, IEnumerable<TPropertyIncluded>>>[] includes);
        TAggregate GetActive(Guid id);
        TAggregate GetActive<TPropertyIncluded>(Guid id, params Expression<Func<TAggregate, IEnumerable<TPropertyIncluded>>>[] includes);

        IQueryable<TAggregate> Search();
        IQueryable<TAggregate> Search<TPropertyIncluded>(params Expression<Func<TAggregate, IEnumerable<TPropertyIncluded>>>[] includes);
        IQueryable<TAggregate> SearchActive();
        IQueryable<TAggregate> SearchActive<TPropertyIncluded>(params Expression<Func<TAggregate, IEnumerable<TPropertyIncluded>>>[] includes);

        void Add(TAggregate aggregate);
        void Update(TAggregate aggregate);
        void Remove(TAggregate aggregate);

        void Deactivate(TAggregate aggregate);
    }
}