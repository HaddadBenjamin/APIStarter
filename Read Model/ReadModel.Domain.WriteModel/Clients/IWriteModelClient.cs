using System.Data.SqlClient;

namespace ReadModel.Domain.WriteModel.Clients
{
    public interface IWriteModelClient
    {
        SqlConnection CreateConnection();
    }
}