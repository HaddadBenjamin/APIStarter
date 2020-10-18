using Nest;

namespace ReadModel.Domain.Interfaces
{
    public interface IReadModelClient
    {
        public ElasticClient ElasticClient { get; }
    }
}