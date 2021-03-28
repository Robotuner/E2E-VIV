using ElectionAPI.DataContext;
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
    public class PartyServiceTests : BaseServiceTests
    {
        private PartyService PartyService = new PartyService();

        [SetUp]
        public override void Setup()
        {
            base.Setup();
        }

        [Test]
        public async Task TestGetAll()
        {
            try
            {
                IEnumerable<Party> ans = await PartyService.GetAll(Context);

                //Assert.AreEqual(6, ans.Count(n => n.Active), "Expected 6 active parties in test dataset.");
                Assert.AreEqual("Democratic Party", ans.SingleOrDefault(n => n.Id == 1 && n.Active).Description, "Expected Id: 1 to be 'Democratic Party' and active");
                Assert.AreEqual("Republican Party", ans.SingleOrDefault(n => n.Id == 2 && n.Active).Description, "Expected Id: 2 to be 'Republican Party' and active");
                Assert.AreEqual("Libertarian Party", ans.SingleOrDefault(n => n.Id == 3 && n.Active).Description, "Expected Id: 3to be 'Libertarian Party' and active");
                Assert.AreEqual("Green Party", ans.SingleOrDefault(n => n.Id == 4 && n.Active).Description, "Expected Id: 4 to be 'Green Party' and active");
                Assert.AreEqual("Socialism and Liberation Party", ans.SingleOrDefault(n => n.Id == 5 && n.Active).Description, "Expected Id: 5 to be 'Socialism and Liberation Party' and active");
                Assert.AreEqual("Socialist Workers Party", ans.SingleOrDefault(n => n.Id == 6 && n.Active).Description, "Expected Id: 6 to be 'Socialist Workers Party' and active");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestGetById()
        {
            try
            {
                Party result = await PartyService.GetByID(Context, 2);

                Assert.IsNotNull(result, "Expect to find Republican Party");
                Assert.IsTrue(result.Active, "Expect Republican Party is Active");
                Assert.AreEqual("Republican Party", result.Description, "Expect description to be Republican Party");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestPartyInsert()
        {
            try
            {
                UOW.BeginTransaction();
                Party catType = new Party()
                {
                    Description = "My Test Party",
                    Active = true
                };
                Party result = await PartyService.Insert(UOW, catType);
                UOW.CloseTransaction();
                Assert.IsNotNull(result, "Expect new Party to be inserted.");
                Assert.IsTrue(result.Id > 6, "Expect Inserted Id to be > 6");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestPartyUpdate()
        {
            try
            {
                UOW.BeginTransaction();
                Party result = await PartyService.GetByID(Context, 3);

                Assert.IsNotNull(result, "Expect to find Party State");
                Assert.AreEqual(result.Description, "Libertarian Party", "Expect to find Party Description = Libertarian Party");
                result.Description = "Libertarian Test Update";
                Party updated = await PartyService.Update(UOW, result);
                UOW.CloseTransaction();
                Assert.IsNotNull(updated, "Expect new Party to be updated.");
                Assert.AreEqual(updated.Description, result.Description, "Expect for Description to be changed to 'Libertarian Test Update'");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestPartyDelete()
        {
            try
            {
                UOW.BeginTransaction();
                Party result = await PartyService.GetByID(Context, 1);
                Assert.IsNotNull(result, "Expect to find Democratic Party");
                Assert.AreEqual(result.Description, "Democratic Party", "Expect to find Party Description = Democratic Party");
                Party deleted = await PartyService.Delete(UOW, result.Id);
                UOW.CloseTransaction();
                Assert.IsTrue(deleted.Active == false, "Expect new Party active flag to be false.");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }
    }
}
