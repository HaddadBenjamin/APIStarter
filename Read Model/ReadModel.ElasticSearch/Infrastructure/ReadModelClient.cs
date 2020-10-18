using System;
using Nest;
using ReadModel.ElasticSearch.Domain.Configurations;
using ReadModel.ElasticSearch.Domain.Interfaces;

namespace ReadModel.ElasticSearch.Infrastructure
{
    public class ReadModelClient : IReadModelClient
    {
        public ReadModelClient(ReadModelConfiguration readModelConfiguration)
        {
            var settings = new ConnectionSettings(new Uri(readModelConfiguration.Uri));
            settings.BasicAuthentication(readModelConfiguration.Username, readModelConfiguration.Password);

            ElasticClient = new ElasticClient(settings);
        }

        public ElasticClient ElasticClient { get; }
    }
}