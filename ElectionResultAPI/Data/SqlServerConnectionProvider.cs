using System.Data;
using System.Data.SqlClient;

namespace ElectionResultAPI.Data
{
    public class SqlServerConnectionProvider : IDatabaseProvider
    {
        private readonly string connectionString;
        public SqlServerConnectionProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection DbContext
        {
            get
            {
                return new SqlConnection(this.connectionString);
            }
        }
    }
}
