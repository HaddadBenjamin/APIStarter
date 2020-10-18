using System;
using System.Collections.Generic;
using Nest;

namespace ReadModel.ElasticSearch
{
    public class IndexMapper : IIndexMapper
    {
        private Dictionary<IndexType, Func<CreateIndexDescriptor, CreateIndexDescriptor>> Mappers = new Dictionary<IndexType, Func<CreateIndexDescriptor, CreateIndexDescriptor>>
        {
            {IndexType.Item, ItemMapper },
            {IndexType.AuditRequest, AuditRequestMapper }
        };

        public CreateIndexDescriptor Map(IndexType indexType, CreateIndexDescriptor createIndexDescriptor) => Mappers[indexType](createIndexDescriptor);

        public static CreateIndexDescriptor ItemMapper(CreateIndexDescriptor createIndexDescriptor) => createIndexDescriptor
            .Map<Item>(typeMappingDescriptor => typeMappingDescriptor
                .AutoMap()
                .Properties(propertiesDescriptor => propertiesDescriptor
                    .Text(textPropertyDescriptor => textPropertyDescriptor.Name(item => item.Name))
                )
            );

        public static CreateIndexDescriptor AuditRequestMapper(CreateIndexDescriptor createIndexDescriptor) =>
            createIndexDescriptor.Map<AuditRequest>(typeMappingDescriptor => typeMappingDescriptor.AutoMap());
    }
}