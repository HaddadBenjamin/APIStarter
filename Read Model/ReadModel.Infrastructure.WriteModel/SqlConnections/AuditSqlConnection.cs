using System.Data.SqlClient;
using ReadModel.Domain.WriteModel.Configurations;
using ReadModel.Domain.WriteModel.SqlConnections;

namespace ReadModel.Infrastructure.WriteModel.SqlConnections
{
    public class AuditSqlConnection : IAuditSqlConnection
    {
        private readonly AuditConfiguration _configuration;

        public AuditSqlConnection(AuditConfiguration configuration) => _configuration = configuration;

        public SqlConnection CreateSqlConnection() => new SqlConnection(_configuration.ConnectionString);
    }
}