using ElectionAPI.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace ElectionAPI.Controllers
{
    public interface IBaseController
    {
        IDbConnection Context { get; set; }
        IUnitOfWork UOW { get; set; }
    }


    public class BaseController : ControllerBase, IBaseController
    {
        public IDbConnection Context { get; set; }
        public IUnitOfWork UOW { get; set; }

        public BaseController(IConfiguration config) : base()
        {
            string cstring = config.GetConnectionString("DefaultConnection");
            this.Context = new SqlConnection(cstring);
            this.UOW = new UnitOfWork(this.Context);
        }


    }
}
