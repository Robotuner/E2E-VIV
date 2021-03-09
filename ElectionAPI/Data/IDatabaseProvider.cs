using System.Data;

namespace ElectionAPI.Data
{
    public interface IDatabaseProvider
    {
        IDbConnection DbContext { get; }
    }
}
