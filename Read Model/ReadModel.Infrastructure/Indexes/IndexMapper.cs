﻿using System;
using System.Collections.Generic;
using Nest;
using ReadModel.Domain;
using ReadModel.Domain.Index;
using ReadModel.Domain.Indexes;

namespace ReadModel.Infrastructure.Indexes
{
    public class IndexMapper : IIndexMapper
    {
        private static readonly Dictionary<IndexType, Func<CreateIndexDescriptor, CreateIndexDescriptor>> Mappers = new Dictionary<IndexType, Func<CreateIndexDescriptor, CreateIndexDescriptor>>
        {
            { IndexType.Item, ItemMapper },
            { IndexType.HttpRequest, HttpRequestMapper }
        };

        public CreateIndexDescriptor Map(IndexType indexType, CreateIndexDescriptor createIndexDescriptor) => Mappers[indexType](createIndexDescriptor);

        private static CreateIndexDescriptor ItemMapper(CreateIndexDescriptor createIndexDescriptor) =>
            createIndexDescriptor.Map<Item>(typeMappingDescriptor => typeMappingDescriptor.AutoMap());

        private static CreateIndexDescriptor HttpRequestMapper(CreateIndexDescriptor createIndexDescriptor) =>
            createIndexDescriptor.Map<HttpRequest>(typeMappingDescriptor => typeMappingDescriptor.AutoMap().Properties(p => p
                .Ip(s => s.Name(n => n.IPv4))
                .GeoPoint(s => s.Name(n => n.Location))
                .Keyword(s => s.Name(n => n.Duration.Suffix("keyword")))
                .Keyword(s => s.Name(n => n.Date.Suffix("keyword")))));
    }
}