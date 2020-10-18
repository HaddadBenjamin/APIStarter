using System;
using Nest;
using ReadModel.Domain.Configurations;
using ReadModel.Domain.Interfaces;

namespace ReadModel.Infrastructure
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