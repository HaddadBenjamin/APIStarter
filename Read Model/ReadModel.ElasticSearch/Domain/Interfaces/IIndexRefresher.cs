﻿using System;
using System.Threading.Tasks;

namespace ReadModel.ElasticSearch.Domain.Interfaces
{
    public interface IIndexRefresher
    {
        Task RefreshAllIndexesAsync();
        Task RefreshIndexAsync(IndexType indexType);
        Task RefreshDocumentAsync(IndexType indexType, Guid id);
    }
}