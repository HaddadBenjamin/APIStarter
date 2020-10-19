using System.Data.SqlClient;

namespace ReadModel.Domain.WriteModel.SqlConnections
{
    public interface IWriteModelSqlConnection
    {
        SqlConnection CreateConnection();
    }
}