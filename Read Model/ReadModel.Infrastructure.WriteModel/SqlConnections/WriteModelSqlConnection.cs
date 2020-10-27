using System.Data.SqlClient;
using ReadModel.Domain.WriteModel.Configurations;
using ReadModel.Domain.WriteModel.SqlConnections;

namespace ReadModel.Infrastructure.WriteModel.SqlConnections
{
    public class WriteModelSqlConnection : IWriteModelSqlConnection
    {
        private readonly WriteModelConfiguration _configuration;

        public WriteModelSqlConnection(WriteModelConfiguration configuration) => _configuration = configuration;

        public SqlConnection CreateSqlConnection() => new SqlConnection(_configuration.ConnectionString);
    }
}