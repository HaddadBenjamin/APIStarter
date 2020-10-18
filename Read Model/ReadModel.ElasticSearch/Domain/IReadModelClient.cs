using Nest;

namespace ReadModel.ElasticSearch
{
    public interface IReadModelClient
    {
        public ElasticClient ElasticClient { get; }
    }
}