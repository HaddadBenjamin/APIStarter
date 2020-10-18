using System;
using Nest;
using ReadModel.ElasticSearch.Domain;
using ReadModel.ElasticSearch.Domain.Configurations;

namespace ReadModel.ElasticSearch.Infrastructure
{
    public class ReadModelClient : IReadModelClient
    {
        public ReadModelClient(ReadModelClientConfiguration clientConfiguration)
        {
            var settings = new ConnectionSettings(new Uri(clientConfiguration.Uri));
            settings.BasicAuthentication(clientConfiguration.Username, clientConfiguration.Password);

            ElasticClient = new ElasticClient(settings);
        }

        public ElasticClient ElasticClient { get; }
    }
}