using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ReadModel.Domain;
using ReadModel.Domain.Index;
using ReadModel.Domain.Indexes;
using ReadModel.Domain.WriteModel.Views;

namespace ReadModel.Infrastructure.Indexes
{
    public class ViewToDocumentMapper : IViewToDocumentMapper
    {
        private readonly IMapper _mapper;
        private readonly Dictionary<IndexType, Func<dynamic, dynamic>> _indexMappers;

        public ViewToDocumentMapper(IMapper mapper)
        {
            _mapper = mapper;
            _indexMappers = new Dictionary<IndexType, Func<dynamic, dynamic>>
            {
                { IndexType.HttpRequest, ToHttpRequest },
                { IndexType.Item, ToItem }
            };
        }

        public IReadOnlyCollection<dynamic> Map(IReadOnlyCollection<dynamic> views, IndexType indexType)
        {
            var indexMapper = _indexMappers[indexType];

            return views.Select(indexMapper).ToList();
        }

        public dynamic Map(dynamic view, IndexType indexType) => _indexMappers[indexType];

        private HttpRequest ToHttpRequest(dynamic view) => _mapper.Map<HttpRequest>((HttpRequestView)view);

        private Item ToItem(dynamic view) => _mapper.Map<Item>((ItemView)view);
    }
}