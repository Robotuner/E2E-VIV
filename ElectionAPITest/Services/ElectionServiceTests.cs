using ElectionAPI.Service;
using ElectionModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectionAPITest.Services
{
    [TestFixture]
    public class ElectionServiceTests : BaseServiceTests
    {
        private ElectionService electionService;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            CategoryService categoryService = new CategoryService();
            PartyService partyService = new PartyService();
            TicketService ticketService = new TicketService();
            CategoryTypeService categoryTypeService = new CategoryTypeService();
            electionService = new ElectionService(categoryService, categoryTypeService, partyService, ticketService);
        }

        [Test]
        public async Task TestGetAll()
        {
            try
            {
                List<Election> ans = (await electionService.GetAll(Context)).ToList();
                Assert.IsNotNull(ans, "There is at least one election in the default dataset");
                Assert.IsTrue(ans[0].Id == DefaultElectionId, "Expecting the defaultElection to have Id of " + DefaultElectionId);
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
                Election result = await electionService.GetByID(Context, DefaultElectionId);
                Assert.IsNotNull(result, "Expect to find the default Election");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestGetFullElection()
        {
            try
            {
                Election result = await electionService.GetFullElection(Context, DefaultElectionId);
                Assert.IsNotNull(result, "Expect to find the default Election");
                Assert.IsTrue(result.CategoryList.Count == 142, "Expect default dataset to have 142 categories, found " + result.CategoryList.Count);
                Assert.IsTrue(result.PartyList.Count >= 6, "Expect default dataset to have 6 parties has " + result.PartyList.Count);
                int totaltickets = 0;
                foreach (Category cat in result.CategoryList)
                {
                    totaltickets += cat.Tickets.Count;
                }
                Assert.IsTrue(totaltickets == 379, "Expect default dataset to have 379 tickets, found " + totaltickets);
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
                Election election = new Election()
                {
                    Date = new DateTime(2022, 11, 3).Date,
                    StartDateLocal = new DateTime(2022, 11, 1, 8, 0, 0),
                    EndDateLocal = new DateTime(2022, 11, 3, 20, 0, 0),
                    Description = "2022 November Election",
                    Version = "0.0.0.0",
                    AllowUpdates = false
                };
                UOW.BeginTransaction();
                Election inserted = await electionService.Insert(UOW, election);
                UOW.CloseTransaction();
                Assert.IsNotNull(inserted);
                Assert.IsTrue(inserted.Id != Guid.Empty, "Expect inserted Election Id not be empty");
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
                Election election = new Election()
                {
                    Date = new DateTime(2022, 11, 3).Date,
                    StartDateLocal = new DateTime(2022, 11, 1, 8, 0, 0),
                    EndDateLocal = new DateTime(2022, 11, 3, 20, 0, 0),
                    Description = "2022 November Election",
                    Version = "0.0.0.0",
                    AllowUpdates = false
                };
                UOW.BeginTransaction();
                Election inserted = await electionService.Insert(UOW, election);
                Assert.IsNotNull(inserted);
                Assert.IsTrue(inserted.Id != Guid.Empty, "Expect inserted Election Id not be empty");
                inserted.Date = new DateTime(2023, 11, 3).Date;
                inserted.StartDateLocal = new DateTime(2023, 11, 1, 8, 0, 0);
                inserted.EndDateLocal = new DateTime(2023, 11, 3, 20, 0, 0);
                inserted.Description = "2023 November Election";
                inserted.AllowUpdates = true;
                Election updated = await electionService.Update(UOW, inserted);
                UOW.CloseTransaction();
                Assert.IsNotNull(updated, "Expected election to be updated");
                Assert.IsTrue(updated.Date.Year == 2023, "Expected election year to be 2023");
                Assert.IsTrue(updated.StartDateLocal.Year == 2023, "Expected election year to be 2023");
                Assert.IsTrue(updated.EndDateLocal.Year == 2023, "Expected election year to be 2023");
                Assert.IsTrue(updated.Description == "2023 November Election", "Expected desription to be updated");
                Assert.IsTrue(updated.AllowUpdates, "Expected allowUpdates to be true");
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
                UOW.BeginTransaction();
                Election result = await electionService.Delete(UOW, DefaultElectionId);
                UOW.CloseTransaction();
                Assert.IsNull(result);
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestSaveAllElection()
        {
            try
            {
                Guid newElectionGuid = Guid.NewGuid();
                Guid newCategoryGuid = Guid.NewGuid();
                List<Ticket> ticketList = new List<Ticket>()
                {
                    new Ticket()
                    {
                        Id = Guid.NewGuid(),
                        ElectionId = newElectionGuid,
                        CategoryId = newCategoryGuid,
                        Description = "newTicketForCategory",
                        PartyId = 2
                    }
                };
                List<Category> categoryList = new List<Category>()
                {
                    new Category()
                    {
                        Id = newCategoryGuid,
                        ElectionId = newElectionGuid,
                        CategoryTypeId = CategoryTypeEnum.federal,
                        Heading="TestNewElectionCategory",
                        Sequence=1,
                        JudgePosition=2,
                        Tickets = ticketList,
                    }
                };
                Election election = new Election()
                {
                    Id = newElectionGuid,
                    Date = new DateTime(2022, 11, 3).Date,
                    StartDateLocal = new DateTime(2022, 11, 1, 8, 0, 0),
                    EndDateLocal = new DateTime(2022, 11, 3, 20, 0, 0),
                    Description = "2022 November Election",
                    Version = "0.0.0.0",
                    AllowUpdates = false,
                    CategoryList = categoryList
                };
                UOW.BeginTransaction();
                Election result = await electionService.SaveAllElection(UOW, election);
                
                Assert.IsNotNull(election, "Expected election to be saved");
                Assert.IsTrue(election.CategoryList.Count == 1, "Expected Category to be saved.");
                Assert.IsTrue(election.CategoryList[0].Tickets.Count == 1, "Expect one Ticket in category");

                CategoryService categoryService = new CategoryService();
                List<Category> clist = (await categoryService.GetByElection(Context, newElectionGuid)).ToList();
                Assert.IsTrue(clist.Count == 1, "Expect one Category");
                TicketService ticketService = new TicketService();
                List<Ticket> tlist = (await ticketService.GetByElection(Context, newElectionGuid)).ToList();
                Assert.IsTrue(tlist.Count == 1, "Expect one Ticket");
                UOW.CloseTransaction();
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }
    }
}
