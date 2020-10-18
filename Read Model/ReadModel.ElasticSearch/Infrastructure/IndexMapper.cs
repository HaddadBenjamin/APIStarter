using System;
using System.Collections.Generic;
using Nest;
using ReadModel.ElasticSearch.Domain;
using ReadModel.ElasticSearch.Domain.Interfaces;
using ReadModel.ElasticSearch.Domain.Models;

namespace ReadModel.ElasticSearch.Infrastructure
{
    public class IndexMapper : IIndexMapper
    {
        private readonly Dictionary<IndexType, Func<CreateIndexDescriptor, CreateIndexDescriptor>> Mappers = new Dictionary<IndexType, Func<CreateIndexDescriptor, CreateIndexDescriptor>>
        {
            { IndexType.Item, ItemMapper },
            { IndexType.AuditRequest, AuditRequestMapper }
        };

        public CreateIndexDescriptor Map(IndexType indexType, CreateIndexDescriptor createIndexDescriptor) => Mappers[indexType](createIndexDescriptor);

        public static CreateIndexDescriptor ItemMapper(CreateIndexDescriptor createIndexDescriptor) => createIndexDescriptor
            .Map<Item>(typeMappingDescriptor => typeMappingDescriptor.AutoMap());

        public static CreateIndexDescriptor AuditRequestMapper(CreateIndexDescriptor createIndexDescriptor) =>
            createIndexDescriptor.Map<AuditRequest>(typeMappingDescriptor => typeMappingDescriptor.AutoMap());
    }
}