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
        private readonly Dictionary<IndexType, Func<object, object>> _indexMappers;

        public ViewToDocumentMapper(IMapper mapper)
        {
            _mapper = mapper;
            _indexMappers = new Dictionary<IndexType, Func<object, object>>
            {
                { IndexType.HttpRequest, ToHttpRequest },
                { IndexType.Item, ToItem }
            };
        }

        public IReadOnlyCollection<object> Map(IReadOnlyCollection<object> views, IndexType indexType)
        {
            var indexMapper = _indexMappers[indexType];

            return views.Select(indexMapper).ToList();
        }

        public object Map(object view, IndexType indexType) => _indexMappers[indexType];

        private HttpRequest ToHttpRequest(object view) => _mapper.Map<HttpRequest>((HttpRequestView)view);

        private Item ToItem(object view) => _mapper.Map<Item>((ItemView)view);
    }
}