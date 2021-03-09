namespace ElectionResultAPI.Data
{
    public class ElectionDBProvider : SqlServerConnectionProvider
    {
        public ElectionDBProvider(string connectionString) : base(connectionString)
        {

        }
    }
}
