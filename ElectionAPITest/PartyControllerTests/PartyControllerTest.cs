
using ElectionAPI.Controllers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Moq;
using ElectionAPI.Repository;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using ElectionModels;
using System;
using System.Linq;

namespace ElectionAPITest.PartyControllerTests
{
    [TestFixture]
    public class PartyControllerTest : BaseControllerTest
    {
        Mock<IPartyRepository> mockIPartyRepository;
        PartyController partyController;
        List<Party> mockPartyList;

        [SetUp]
        public override void Setup()
        {
            this.Setup();
            mockIPartyRepository = new Mock<IPartyRepository>();
            partyController = new PartyController(mockIConfiguration.Object, mockIPartyRepository.Object);
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
        public async Task PartyControllerGetAll()
        {
            mockIPartyRepository.Setup(r => r.GetAll(It.IsAny<IDbConnection>())).Returns(Task.FromResult(mockPartyList));
            List<Party> partyList = await partyController.Get();
            Assert.AreEqual(partyList, mockPartyList);
        }

        [Test]
        public async Task PartyControllerGetById()
        {
            int testId = 1;
            mockIPartyRepository.Setup(r => r.GetByID(It.IsAny<IDbConnection>(), testId)).Returns(Task.FromResult(mockPartyList.SingleOrDefault(n => n.Id == testId)));
            Party party = await partyController.GetById(1);
            Assert.AreEqual(party, mockPartyList.SingleOrDefault(n => n.Id == testId));
        }

        [Test]
        public async Task PartyControllerUpdate()
        {            
            int testId = 1;
            Party UpdatedParty = mockPartyList.SingleOrDefault(n => n.Id == testId);
            UpdatedParty.Description = "Libertarian";
            mockIPartyRepository.Setup(r => r.Update(It.IsAny<IDbConnection>(), UpdatedParty)).Returns(Task.FromResult(mockPartyList.SingleOrDefault(n => n.Id == testId)));
            Party party = await partyController.Update(UpdatedParty);
            Assert.AreEqual(party, mockPartyList.SingleOrDefault(n => n.Id == testId));
        }

        [Test]
        public async Task PartyControllerDelete()
        {
            int testId = 1;
            mockIPartyRepository.Setup(r => r.Delete(It.IsAny<IDbConnection>(), testId)).Returns(Task.FromResult((Party)null));
            bool result = await partyController.Delete(testId);
            Assert.IsTrue(result);
        }
    }
}