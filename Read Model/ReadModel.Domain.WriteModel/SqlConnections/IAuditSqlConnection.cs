using System.Data.SqlClient;

namespace ReadModel.Domain.WriteModel.SqlConnections
{
    public interface IAuditSqlConnection
    {
        SqlConnection CreateConnection();
    }
}