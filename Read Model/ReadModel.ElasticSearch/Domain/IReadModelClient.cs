using Nest;

namespace ReadModel.ElasticSearch.Domain
{
    public interface IReadModelClient
    {
        public ElasticClient ElasticClient { get; }
    }
}