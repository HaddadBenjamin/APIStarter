using System;
using Nest;
using ReadModel.Domain.Clients;
using ReadModel.Domain.Configurations;

namespace ReadModel.Infrastructure.Clients
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