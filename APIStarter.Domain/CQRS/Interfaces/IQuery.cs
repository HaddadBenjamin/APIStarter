using MediatR;

namespace APIStarter.Domain.CQRS.Interfaces
{
    public interface IQuery<TQueryResult> : IRequest<TQueryResult> { }
}