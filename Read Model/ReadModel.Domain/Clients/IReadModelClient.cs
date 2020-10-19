using Nest;

namespace ReadModel.Domain.Clients
{
    public interface IReadModelClient
    {
        public ElasticClient ElasticClient { get; }
    }
}