using System;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Clients;
using ReadModel.Domain.Configurations;
using ReadModel.Domain.Index.HttpRequest;
using ReadModel.Domain.Index.Item;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Clients
{
    public class ReadModelClient : IReadModelClient
    {
        public ReadModelClient(ReadModelConfiguration readModelConfiguration, IIndexName indexName)
        {
            var settings = new ConnectionSettings(new Uri(readModelConfiguration.Uri))
                .BasicAuthentication(readModelConfiguration.Username, readModelConfiguration.Password)
                .DefaultMappingFor<Item>(descriptor => descriptor.IndexName(indexName.GetIndexName(IndexType.Item)))
                .DefaultMappingFor<HttpRequest>(descriptor => descriptor.IndexName(indexName.GetIndexName(IndexType.HttpRequest)));

            ElasticClient = new ElasticClient(settings);
        }

        public ElasticClient ElasticClient { get; }
    }
}