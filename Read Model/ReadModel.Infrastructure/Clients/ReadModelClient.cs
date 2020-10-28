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
        public ElasticClient ElasticClient { get; }

        public ReadModelClient(ReadModelConfiguration readModelConfiguration)
        {
            var settings = new ConnectionSettings(new Uri(readModelConfiguration.Uri))
                .BasicAuthentication(readModelConfiguration.Username, readModelConfiguration.Password)
                .DefaultMappingFor<Item>(descriptor => descriptor.IndexName(IndexNameWithoutAlias.IndexName(IndexType.Item)))
                .DefaultMappingFor<Item>(descriptor => descriptor.IndexName(IndexNameWithoutAlias.TemporaryIndexName(IndexType.Item)))
                .DefaultMappingFor<HttpRequest>(descriptor => descriptor.IndexName(IndexNameWithoutAlias.IndexName(IndexType.HttpRequest)))
                .DefaultMappingFor<HttpRequest>(descriptor => descriptor.IndexName(IndexNameWithoutAlias.TemporaryIndexName(IndexType.HttpRequest)));

            ElasticClient = new ElasticClient(settings);
        }
    }
}