using System.Data.SqlClient;
using ReadModel.Domain.WriteModel.Clients;
using ReadModel.Domain.WriteModel.Configurations;

namespace ReadModel.Infrastructure.WriteModel.Clients
{
    public class AuditClient : IAuditClient
    {
        private readonly AuditConfiguration _configuration;

        public AuditClient(AuditConfiguration configuration) => _configuration = configuration;

        public SqlConnection CreateConnection() => new SqlConnection(_configuration.ConnectionString);
    }
}