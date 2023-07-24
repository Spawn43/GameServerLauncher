using System.Data;
using Microsoft.Data.SqlClient;
namespace GameServerLauncher.DbConnections
{
    public interface ISQLDbConnection
    {
        IDbConnection Connect();
    }

    public class SQLDbConnection : ISQLDbConnection
    {

        private readonly string _connectionString;

        public SQLDbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection Connect()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
