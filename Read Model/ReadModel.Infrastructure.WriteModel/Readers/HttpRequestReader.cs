using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ReadModel.Domain.Exceptions;
using ReadModel.Domain.WriteModel.Readers;
using ReadModel.Domain.WriteModel.SqlConnections;
using ReadModel.Domain.WriteModel.Views;
using ReadModel.Infrastructure.WriteModel.SqlQueries;

namespace ReadModel.Infrastructure.WriteModel.Readers
{
    public class HttpRequestReader : IHttpRequestReader
    {
        private readonly IAuditSqlConnection _sqlConnection;

        public HttpRequestReader(IAuditSqlConnection sqlConnection) => _sqlConnection = sqlConnection;

        public async Task<IReadOnlyCollection<HttpRequestView>> GetAllAsync() => await Search(new SearchParameters());

        public async Task<HttpRequestView> GetByIdAsync(Guid id)
        {
            var httpRequestView = (await Search(new SearchParameters { Id = id })).FirstOrDefault();

            if (httpRequestView is null)
                throw new NotFoundException(nameof(HttpRequestView));

            return httpRequestView;
        }

        private async Task<IReadOnlyCollection<HttpRequestView>> Search(SearchParameters searchParameters)
        {
            await using var sqlConnection = _sqlConnection.CreateConnection();

            return (await sqlConnection.QueryAsync<HttpRequestView>(AuditSqlQueries.SearchHttpRequests, searchParameters)).ToList();
        }
    }
}