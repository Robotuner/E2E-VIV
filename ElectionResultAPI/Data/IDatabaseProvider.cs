using System.Data;

namespace ElectionResultAPI.Data
{
    public interface IDatabaseProvider
    {
        IDbConnection DbContext { get; }
    }
}
