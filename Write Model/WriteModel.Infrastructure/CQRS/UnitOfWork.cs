using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using WriteModel.Domain.Audit.Services;
using WriteModel.Domain.CQRS;
using WriteModel.Domain.CQRS.Interfaces;

namespace WriteModel.Infrastructure.CQRS
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly IDatabaseChangesAuditService _databaseChangesAuditService;
        private Hashtable _repositories = new Hashtable();
        private IDbContextTransaction _transaction;

        public UnitOfWork(TDbContext dbContext, IDatabaseChangesAuditService databaseChangesAuditService)
        {
            _dbContext = dbContext;
            _databaseChangesAuditService = databaseChangesAuditService;
        }

        public IRepository<TAggregate> Repository<TAggregate>() where TAggregate : AggregateRoot
        {
            var aggregateTypeName = typeof(TAggregate).Name;

            if (!_repositories.ContainsKey(aggregateTypeName))
                _repositories.Add(aggregateTypeName, Activator.CreateInstance(typeof(IRepository<>).MakeGenericType(typeof(TAggregate)), _dbContext));

            return (IRepository<TAggregate>)_repositories[aggregateTypeName];
        }

        public async Task SaveChangesAsync() => Task.WaitAll(_dbContext.SaveChangesAsync(), _databaseChangesAuditService.Audit());

        public void BeginTransaction() => _transaction = _dbContext.Database.BeginTransaction();

        public void CommitTransaction()
        {
            try
            {
                _dbContext.SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}