using APIStarter.Domain.AuthentificationContext;
using APIStarter.Domain.CQRS;
using APIStarter.Domain.CQRS.Interfaces;

namespace APIStarter.Infrastructure.CQRS
{
    public class Session<TAggregate, TRepository> :
        InternalSession<TAggregate>,
        ISession<TAggregate, TRepository>
        where TAggregate : AggregateRoot
        where TRepository : IRepository<TAggregate>
    {
        public Session(TRepository repository, IAuthentificationContext authentificationContext, IMediator mediator) :
            base(repository, authentificationContext, mediator) =>
            Repository = repository;

        public TRepository Repository { get; }
    }

    public class Session<TAggregate> :
        InternalSession<TAggregate>,
        ISession<TAggregate>
        where TAggregate : AggregateRoot
    {
        public Session(IRepository<TAggregate> repository, IAuthentificationContext authentificationContext, IMediator mediator) :
            base(repository, authentificationContext, mediator) { }
    }
}
