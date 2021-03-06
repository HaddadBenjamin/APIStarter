﻿using System;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Configuration;
using WriteModel.Domain.ReadModel;
using WriteModel.Domain.ReadModel.Apis;
using WriteModel.Domain.ReadModel.Configurations;
using WriteModel.Infrastructure.Polly;

namespace WriteModel.Infrastructure.ReadModel.Apis
{
    public class ReadModelApi : IReadModelApi
    {
        private readonly IFlurlClient _flurlClient;

        public ReadModelApi(IFlurlClientFactory flurlClientFactory, ReadModelConfiguration readModelConfiguration) => _flurlClient = flurlClientFactory
            .Get(readModelConfiguration.Uri)
            .WithHeader("Content-Type", "application/json");

        public async Task RefreshAllAsync() =>
            await ResilientPolicies.ExponentialRetryAsync(_flurlClient.Request("refresh/indexes").PostAsync(null));

        public async Task RefreshIndexAsync(IndexType indexType) =>
            await ResilientPolicies.ExponentialRetryAsync(_flurlClient.Request($"refresh/indexes/{(int)indexType}").PostAsync(null));

        public async Task RefreshDocumentAsync(IndexType indexType, Guid id) =>
            await ResilientPolicies.ExponentialRetryAsync(_flurlClient.Request($"refresh/indexes/{(int)indexType}/{id}").PostAsync(null));
    }
}
