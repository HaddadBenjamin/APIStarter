using System.Data.SqlClient;

namespace ReadModel.Domain.WriteModel.SqlConnections
{
    public interface ICreateSqlConnection
    {
        SqlConnection CreateSqlConnection();
    }
}