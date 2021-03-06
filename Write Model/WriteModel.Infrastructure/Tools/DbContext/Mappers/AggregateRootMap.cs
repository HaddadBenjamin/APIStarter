﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WriteModel.Domain.CQRS;

namespace WriteModel.Infrastructure.Tools.DbContext.Mappers
{
    public abstract class AggregateRootMap<TAggregate> where TAggregate : AggregateRoot
    {
        public void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<TAggregate>();

            entity.HasKey(aggregate => aggregate.Id);
            entity.Property(aggregate => aggregate.Id).IsConcurrencyToken();
            entity.HasIndex(aggregate => aggregate.Id);
            entity.HasIndex(aggregate => aggregate.IsActive);

            Map(entity);
        }

        protected abstract void Map(EntityTypeBuilder<TAggregate> entity);
    }
}