using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ReadModel.Domain.WriteModel.Readers;
using ReadModel.Domain.WriteModel.SqlConnections;
using ReadModel.Domain.WriteModel.Views;
using ReadModel.Infrastructure.WriteModel.SqlQueries;

namespace ReadModel.Infrastructure.WriteModel.Readers
{
    public class HttpRequestReader : BaseWriteModelReader<HttpRequestView>
    {
        private readonly IAuditSqlConnection _sqlConnection;

        public HttpRequestReader(IAuditSqlConnection sqlConnection) => _sqlConnection = sqlConnection;

        protected override async Task<IReadOnlyCollection<HttpRequestView>> Search(SearchParameters searchParameters)
        {
            await using var sqlConnection = _sqlConnection.CreateConnection();

            return (await sqlConnection.QueryAsync<HttpRequestView>(AuditSqlQueries.SearchHttpRequests, searchParameters)).ToList();
        }
    }
}