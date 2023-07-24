using Dapper;
using GameServerLauncher.DbConnections;
using GameServerLauncher.Models;
using System.Data;

namespace GameServerLauncher.Repository
{
    public interface IServerStatisticRepository
    {
        Task<IEnumerable<ServerStatistic>>  GetServerStatsitic(int minutes);

        void InsertServerStatstic(ServerStatistic info);
    }
    public class ServerStatisticsRepository : IServerStatisticRepository
    {
        ISQLDbConnection _connection;
        public ServerStatisticsRepository(ISQLDbConnection connection){
            _connection = connection;
        }


        public async Task<IEnumerable<ServerStatistic>> GetServerStatsitic(int minutes)
        {
            using (IDbConnection database = _connection.Connect())
            {

                string sqlQuery = @"
                SELECT TOP (@Count) *
                FROM dbo.ServerStatistics
                ORDER BY Time desc";
                var parameters = new DynamicParameters();
                parameters.Add("Count", 6*minutes);
               
                IEnumerable<ServerStatistic> stats;
                stats = await database.QueryAsync<ServerStatistic>(sqlQuery, parameters);
                return stats;
            }
        }

        public async void InsertServerStatstic(ServerStatistic info)
        {
            using (IDbConnection database = _connection.Connect())
            {
                object paramters = new
                {
                    Time = info.Time,
                    CPU = info.CPU,
                    RAMAmount = info.RAMAmount,
                    RAMUsage = info.RAMUsage,
                    Bandwidth = info.Bandwidth,
                    StorageAmount = info.StorageAmount,
                    StorageUsage = info.StorageUsage,
                };
                database.Execute(@"
                INSERT INTO 
                dbo.ServerStatistics (Time, CPU, RAMAMount, RAMUsage, Bandwidth, StorageAmount, StorageUsage)
                VALUES (@Time, @CPU, @RAMAmount, @RAMUsage, @Bandwidth, @StorageAmount, @StorageUsage)
                ", paramters);
         
            }
        }
    }
}
