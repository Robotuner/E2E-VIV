using ElectionAPI.Repository;
using ElectionAPI.Service;
using ElectionAPITest.Services;
using ElectionModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionAPITest.Repositories
{
    [TestFixture]
    public class TicketRepositoryTests : BaseServiceTests
    {
        protected TicketRepository ticketRepository;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            ticketRepository = new TicketRepository(null, new TicketService());
        }

        [Test]
        public async Task TestGetByElection()
        {
            try
            {
                List<Ticket> ans = await ticketRepository.GetByElection(Context, DefaultElectionId);
                Assert.IsNotNull(ans, "Expected Tickets from default dataset");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestGetByID()
        {
            try
            {
                List<Ticket> ans = await ticketRepository.GetByElection(Context, DefaultElectionId);
                int ndxToGet = rand.Next(0, ans.Count);
                Ticket result = await ticketRepository.GetByID(Context, ans[ndxToGet].Id);
                Assert.IsNotNull(result, "Expect ticket");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestInsert()
        {
            try
            {
                List<Ticket> ans = await ticketRepository.GetByElection(Context, DefaultElectionId);
                int ndxToGet = rand.Next(0, ans.Count);
                Guid categoryId = ans[ndxToGet].CategoryId;
                int partyId = rand.Next(1, 6);

                Ticket ticket = new Ticket()
                {
                    ElectionId = DefaultElectionId,
                    CategoryId = categoryId,
                    Description = "TestTicketInsert",
                    PartyId = partyId,
                    TicketType = ans[ndxToGet].TicketType,
                    Information = "TextTicketInformation",
                    Sequence = 1
                };
                UOW.BeginTransaction();
                Ticket result = await ticketRepository.Insert(UOW, ticket);
                UOW.CloseTransaction();
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Description == ticket.Description, "Expect description to be TestTicketInsert");
                Assert.IsTrue(result.Information == ticket.Information, "Expect description to be TestTicketInformation");
                Assert.IsTrue(result.Id != Guid.Empty, "Expect Id to not be empty");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestUpdate()
        {
            try
            {
                List<Ticket> ans = await ticketRepository.GetByElection(Context, DefaultElectionId);
                int ndxToGet = rand.Next(0, ans.Count);
                Guid categoryId = ans[ndxToGet].CategoryId;
                int partyId = rand.Next(1, 6);

                Ticket ticket = new Ticket()
                {
                    ElectionId = DefaultElectionId,
                    CategoryId = categoryId,
                    Description = "TestTicketInsert",
                    PartyId = partyId,
                    TicketType = ans[ndxToGet].TicketType,
                    Information = "TextTicketInformation",
                    Sequence = 1
                };
                UOW.BeginTransaction();
                Ticket result = await ticketRepository.Insert(UOW, ticket);
                Assert.IsNotNull(result);
                result.Description = "TestTicketUpdate";
                result.Information = "TestTicketUpdate";
                Ticket updated = await ticketRepository.Update(UOW, result);
                UOW.CloseTransaction();
                Assert.IsTrue(result.Description != ticket.Description, "Expect description to be TestTicketUpdate");
                Assert.IsTrue(result.Information != ticket.Information, "Expect description to be TestTicketUpdate");
                Assert.IsTrue(result.Id == updated.Id, "Expect Id to not be empty");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestDelete()
        {
            try
            {
                List<Ticket> ans = await ticketRepository.GetByElection(Context, DefaultElectionId);
                int ndxToGet = rand.Next(0, ans.Count);
                UOW.BeginTransaction();
                Ticket deleted = await ticketRepository.Delete(UOW, ans[ndxToGet].Id);
                List<Ticket> results = (await ticketRepository.GetByElection(Context, DefaultElectionId)).ToList();
                UOW.CloseTransaction();
                Assert.IsNull(deleted, "Expect to not find deleted ticket");
                Assert.IsTrue(results.Count == ans.Count - 1, "Expect total Tickets to be one less that starting value");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }
    }
}
