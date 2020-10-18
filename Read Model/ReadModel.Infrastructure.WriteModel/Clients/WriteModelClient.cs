using System.Data.SqlClient;
using ReadModel.Domain.WriteModel.Clients;
using ReadModel.Domain.WriteModel.Configurations;

namespace ReadModel.Infrastructure.WriteModel.Clients
{
    public class WriteModelClient : IWriteModelClient
    {
        private readonly WriteModelConfiguration _configuration;

        public WriteModelClient(WriteModelConfiguration configuration) => _configuration = configuration;

        public SqlConnection CreateConnection() => new SqlConnection(_configuration.ConnectionString);
    }
}