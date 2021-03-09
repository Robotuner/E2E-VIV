using Dapper;
using ElectionAPI.Controllers;
using ElectionAPI.Repository;
using ElectionAPI.Service;
using ElectionModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionAPITest.PartyControllerTests
{
    [TestFixture]
    public class PartyServiceTest
    {
        //Mock<IConfiguration> mockIConfiguration;
        Mock<IDbConnection> mockIDbConnection;
        //Mock<IPartyRepository> mockIPartyRepository;
        Mock<IPartyService> mockIPartyService;
        //Mock<ILogger<PartyRepository>> mockILogger;
        //PartyRepository partyRepository;
        PartyService partyService;
        //PartyController controller;
        List<Party> mockPartyList;

        [SetUp]
        public void Setup()
        {
            //mockIConfiguration = new Mock<IConfiguration>();
            mockIDbConnection = new Mock<IDbConnection>();
            //mockIPartyRepository = new Mock<IPartyRepository>();
            mockIPartyService = new Mock<IPartyService>();
            //mockILogger = new Mock<ILogger<PartyRepository>>();

            //partyRepository = new PartyRepository(mockILogger.Object, mockIPartyService.Object);
            partyService = new PartyService();
            //controller = new PartyController(mockIConfiguration.Object, mockIPartyRepository.Object);
            mockPartyList = new List<Party>()
            {
                   new Party()
                   {
                       Id = 1,
                       Description= "Democrat"
                   },
                   new Party()
                   {
                        Id = 2,
                        Description= "Republican"
                   }
            };
        }

        //[Test]
        //public async Task PartyServiceGetAll()
        //{
        //    mockIDbConnection.Setup(r => r.QueryAsync<Party>("Party_Get",null,null,null,CommandType.StoredProcedure)).Returns(Task.FromResult(mockPartyList as IEnumerable<Party>));
        //    //mockIPartyService.Setup(r => r.GetAll(It.IsAny<IDbConnection>())).Returns(Task.FromResult(mockPartyList as IEnumerable<Party>));

        //    IEnumerable<Party> result = await partyService.GetAll(It.IsAny<IDbConnection>());
        //    Assert.AreEqual(result, mockPartyList);
        //}

        //[Test]
        //public async Task PartyServiceGetById()
        //{
        //    int testId = 1;
        //    var p = new DynamicParameters();
        //    p.Add("@id", testId, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

        //    mockIDbConnection.Setup(r => r.QueryAsync<Party>("Party_GetById", p, null, null, CommandType.StoredProcedure))
        //        .Returns(Task.FromResult(mockPartyList.Where(n => n.Id == testId)));

        //    IEnumerable<Party> result = await partyService.GetAll(It.IsAny<IDbConnection>());
        //    Assert.AreEqual(result, mockPartyList.FirstOrDefault());
        //}
    }
}
