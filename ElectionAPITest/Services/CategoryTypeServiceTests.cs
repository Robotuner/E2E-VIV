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
    public class CategoryTypeServiceTests : BaseServiceTests
    {
        private CategoryTypeService categoryTypeService = new CategoryTypeService();

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
                IEnumerable<CategoryType> ans = await categoryTypeService.GetAll(Context);

                Assert.AreEqual("Measure", ans.SingleOrDefault(n => n.Id == 1 && n.Active).Description, "Expected Id: 1 to be 'Measure' and active");
                Assert.AreEqual("Federal", ans.SingleOrDefault(n => n.Id == 2 && n.Active).Description, "Expected Id: 2 to be 'Federal' and active");
                Assert.AreEqual("State", ans.SingleOrDefault(n => n.Id == 3 && n.Active).Description, "Expected Id: 3 to be 'State' and active");
                Assert.AreEqual("Legislative", ans.SingleOrDefault(n => n.Id == 4 && n.Active).Description, "Expected Id: 4 to be 'Legislative' and active");
                Assert.AreEqual("Judicial", ans.SingleOrDefault(n => n.Id == 5 && n.Active).Description, "Expected Id: 5 to be 'Judicial' and active");
                await Task.CompletedTask;
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
                CategoryType result = await categoryTypeService.GetByID(Context, 2);

                Assert.IsNotNull(result, "Expect to find Federal Category Type");
                Assert.IsTrue(result.Active, "Expect Federal type is Active");
                Assert.AreEqual("Federal", result.Description, "Expect description to be Federal");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestCategoryTypeInsert()
        {
            try
            {
                UOW.BeginTransaction();
                CategoryType catType = new CategoryType()
                {
                    Description = "TestInsertCategory",
                    Active = true
                };
                CategoryType result = await categoryTypeService.Insert(UOW, catType);
                UOW.CloseTransaction();
                Assert.IsNotNull(result, "Expect new CategoryType to be inserted.");
                Assert.IsTrue(result.Id > 5, "Expect Inserted Id to be > 5");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestCategoryTypeUpdate()
        {
            try
            {
                UOW.BeginTransaction();
                CategoryType result = await categoryTypeService.GetByID(Context, 3);

                Assert.IsNotNull(result, "Expect to find CategoryType State");
                Assert.AreEqual(result.Description, "State", "Expect to find CategoryType Description = State");
                result.Description = "State Test Update";
                CategoryType updated = await categoryTypeService.Update(UOW, result);
                UOW.CloseTransaction();
                Assert.IsNotNull(updated, "Expect new CategoryType to be updated.");
                Assert.AreEqual(updated.Description, result.Description, "Expect for Description to be changed to 'State Test Update'");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestCategoryTypeDelete()
        {
            try
            {
                UOW.BeginTransaction();
                CategoryType result = await categoryTypeService.GetByID(Context, 1);
                Assert.IsNotNull(result, "Expect to find CategoryType Measure");
                Assert.AreEqual(result.Description, "Measure", "Expect to find CategoryType Description = Measure");
                CategoryType deleted = await categoryTypeService.Delete(UOW, result.Id);
                UOW.CloseTransaction();
                Assert.IsNull(deleted, "Expect new CategoryType to be deleted.");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }
    }
}
