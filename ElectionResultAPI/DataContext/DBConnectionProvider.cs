namespace ElectionResultAPI.DataContext
{
    public interface IDbConnectionProvider<TDatabase> where TDatabase : Database
    {
        string ConnectionString { get; }
    }

    public class DbConnectionProvider<TDatabase> : IDbConnectionProvider<TDatabase> where TDatabase : Database
    {
        public string ConnectionString { get; }
        public DbConnectionProvider(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
