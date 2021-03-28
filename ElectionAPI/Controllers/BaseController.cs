using ElectionAPI.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace ElectionAPI.Controllers
{
    public enum DBConnectionEnum
    {
        sqlConnection,
        postgresConnection
    }

    public interface IBaseController
    {
        IDbConnection Context { get; set; }
        IUnitOfWork UOW { get; set; }
    }


    public class BaseController : ControllerBase, IBaseController
    {
        public IDbConnection Context { get; set; }
        public IUnitOfWork UOW { get; set; }

        private DBConnectionEnum dbConnType = DBConnectionEnum.postgresConnection;

        public BaseController(IConfiguration config) : base()
        {
            //string cstring = config.GetConnectionString("DefaultConnection");
            this.Context = GetDbConnection(config);
            this.UOW = new UnitOfWork(this.Context);
        }

        private DbConnection GetDbConnection(IConfiguration config)
        {
            switch (dbConnType)
            {
                case DBConnectionEnum.sqlConnection:
                    return new SqlConnection(config.GetConnectionString("SqlConnection"));
                case DBConnectionEnum.postgresConnection:
                default:
                    return new NpgsqlConnection(config.GetConnectionString("PostgresConnection"));
            }          
        }

    }
}
