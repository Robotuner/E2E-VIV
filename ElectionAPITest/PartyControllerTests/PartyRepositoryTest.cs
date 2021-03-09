using ElectionAPI.Controllers;
using ElectionAPI.Repository;
using ElectionAPI.Service;
using ElectionModels;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ElectionAPITest.PartyControllerTests
{
    [TestFixture]
    public class PartyRepositoryTest
    {
        Mock<IConfiguration> mockIConfiguration;
        Mock<IDbConnection> mockIDbConnection;
        Mock<IPartyRepository> mockIPartyRepository;
        Mock<IPartyService> mockIPartyService;
        Mock<ILogger<PartyRepository>> mockILogger;
        PartyRepository partyRepository;
        PartyController controller;
        List<Party> mockPartyList;

        [SetUp]
        public void Setup()
        {
            mockIConfiguration = new Mock<IConfiguration>();
            mockIDbConnection = new Mock<IDbConnection>();
            mockIPartyRepository = new Mock<IPartyRepository>();
            mockIPartyService = new Mock<IPartyService> ();
            mockILogger = new Mock<ILogger<PartyRepository>>();

            partyRepository = new PartyRepository(mockILogger.Object, mockIPartyService.Object);

            controller = new PartyController(mockIConfiguration.Object, mockIPartyRepository.Object);
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

        [Test]
        public async Task PartyRepositoryGetAll()
        {
            mockIPartyService.Setup(r => r.GetAll(It.IsAny<IDbConnection>())).Returns(Task.FromResult(mockPartyList as IEnumerable<Party>));

            List<Party> result = await partyRepository.GetAll(It.IsAny<IDbConnection>());
            Assert.AreEqual(result, mockPartyList);
        }

        [Test]
        public async Task PartyRepositoryGetByID()
        {
            int testId = 1;
            mockIPartyService.Setup(r => r.GetByID(It.IsAny<IDbConnection>(), testId)).Returns(Task.FromResult(mockPartyList.SingleOrDefault(n => n.Id == testId)));

            Party result = await partyRepository.GetByID(It.IsAny<IDbConnection>(), testId);
            Assert.AreEqual(result, mockPartyList.SingleOrDefault(n => n.Id == testId));
        }

        [Test]
        public async Task PartyRepositoryUpdate()
        {
            int testId = 1;
            Party UpdatedParty = mockPartyList.SingleOrDefault(n => n.Id == testId);
            UpdatedParty.Description = "Libertarian";

            mockIPartyService.Setup(r => r.Update(It.IsAny<IDbConnection>(), UpdatedParty)).Returns(Task.FromResult(mockPartyList.SingleOrDefault(n => n.Id == testId)));

            Party result = await partyRepository.Update(It.IsAny<IDbConnection>(), UpdatedParty);
            Assert.AreEqual(result, mockPartyList.SingleOrDefault(n => n.Id == testId));
        }

        [Test]
        public async Task PartyRepositoryDelete()
        {
            int testId = 1;
 
            mockIPartyService.Setup(r => r.Delete(It.IsAny<IDbConnection>(), testId)).Returns(Task.FromResult((Party)null));

            Party result = await partyRepository.Delete(It.IsAny<IDbConnection>(), testId);
            Assert.IsNull(result);
        }


        [Test]
        public async Task PartyRepositoryInsert()
        {
            Party newParty = new Party()
            {
                Id = 3,
                Description = "Trump Party"
            };
            mockPartyList.Add(newParty);

            mockIPartyService.Setup(r => r.Insert(It.IsAny<IDbConnection>(), newParty)).Returns(Task.FromResult(mockPartyList.Single(n => n.Id==3)));

            Party result = await partyRepository.Insert(It.IsAny<IDbConnection>(), newParty);
            Assert.AreEqual(result, newParty);
        }
    }
}
