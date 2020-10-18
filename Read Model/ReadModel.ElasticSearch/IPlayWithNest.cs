using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;

namespace ReadModel.ElasticSearch
{
    public static class IPlayWithNest 
    {
        public static void Map()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            settings.BasicAuthentication("elastic", "elastic");
            var client = new ElasticClient(settings);

            var deleteItemsIndexResponse = client.Indices.Delete("items");
            var deleteAuditRequestsIndexResponse = client.Indices.Delete("auditrequests");
            var createItemsIndexResponse = client.Indices.Create("items", ItemMapper);
            var createAuditRequestsIndexResponse = client.Indices.Create("auditrequests", AuditRequestMapper);
        }

        public static CreateIndexDescriptor ItemMapper(CreateIndexDescriptor c) => c
            .Map<Item>(m => m
                .AutoMap()
                .Properties(p => p
                    .Text(_ => _.Name(n => n.Name))
                )
            );

        public static CreateIndexDescriptor AuditRequestMapper(CreateIndexDescriptor c) => c
            .Map<AuditRequest>(m => m.AutoMap().Properties(p => 
                p.Text(_ => _.Name(n => n.HttpMethod))));
    }
}
