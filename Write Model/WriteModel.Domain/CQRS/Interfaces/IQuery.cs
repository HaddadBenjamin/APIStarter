using MediatR;

namespace WriteModel.Domain.CQRS.Interfaces
{
    public interface IQuery<TQueryResult> : IRequest<TQueryResult> { }
}