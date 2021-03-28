using ElectionAPI.Controllers;
using ElectionAPI.DataContext;
using Npgsql;
using NUnit.Framework;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ElectionAPITest.Services
{
    public class BaseServiceTests
    {
        protected Guid DefaultElectionId = Guid.Parse("A13ACD4A-D415-4B27-AFE6-E2310AC71BC6");
        protected Guid PresCategoryId = Guid.Parse("90545980-c4bc-48d7-be0a-97730aad3972");
        protected Guid BidenTicketId = Guid.Parse("c7c91842-44f3-4ffc-b755-0346a6534599");
        protected Guid TrumpTicketId = Guid.Parse("b28c5825-71d7-4341-8733-9ccb6c942580");
        protected Random rand = new Random();
        protected IDbConnection Context;
        protected IUnitOfWork UOW;
        private DBConnectionEnum dbConnType = DBConnectionEnum.postgresConnection;
        protected DbConnection GetDbConnection()
        {
            switch (dbConnType)
            {
                case DBConnectionEnum.sqlConnection:
                    return new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Database=ElectionDB;User Id=Robotuner;Password=NSNX2bVu;");
                case DBConnectionEnum.postgresConnection:
                default:
                    return new NpgsqlConnection("User Id=Robotuner;Password=NSNX2bVu;Server=localhost;Port=5432;Database=Election;");
            }
        }

        [SetUp]
        public virtual void Setup()
        {
            this.Context = GetDbConnection();
            this.UOW = new UnitOfWork(this.Context);
        }
    }
}
