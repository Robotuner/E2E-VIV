using Microsoft.Extensions.Logging;
using System.Data;


namespace ElectionAPI.DataContext
{
    public class DapperDbContext<TDatabase> where TDatabase : Database
    {
        private readonly bool ownsConnection;
        private readonly IDbConnection connection;
        private readonly ILogger log;

        public DapperDbContext(string connectionString, ILogger logger)
        {

        }

        public DapperDbContext(IDbConnectionProvider<TDatabase> connectionStringProvider, ILogger logger)
        {

        }
    }
}
