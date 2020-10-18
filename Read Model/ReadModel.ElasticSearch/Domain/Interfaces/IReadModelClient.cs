using Nest;

namespace ReadModel.ElasticSearch.Domain.Interfaces
{
    public interface IReadModelClient
    {
        public ElasticClient ElasticClient { get; }
    }
}