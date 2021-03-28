using ElectionAPI.Repository;
using ElectionAPI.Service;
using ElectionAPITest.Services;
using ElectionModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectionAPITest.Repositories
{
    [TestFixture]
    public class CategoryTypeRepositoryTest : BaseServiceTests
    {
        protected CategoryTypeRepository categoryTypeRepository;

        [SetUp]
        public override void Setup()
        {    
            base.Setup();
            categoryTypeRepository = new CategoryTypeRepository(null, new CategoryTypeService());
        }

        [Test]
        public async Task TestGetAll()
        {
            List<CategoryType> result = await categoryTypeRepository.GetAll(Context);
            Assert.IsNotNull(result, "Expected at least 5 default category types");
        }

        [Test]
        public async Task TestGetById()
        {
            List<CategoryType> result = await categoryTypeRepository.GetAll(Context);
            Assert.IsNotNull(result, "Expected at least 5 default category types");
            int ndx = rand.Next(0, result.Count);
            CategoryType ctype = await categoryTypeRepository.GetByID(Context, result[ndx].Id);
            Assert.IsNotNull(ctype, "Expect to get category type: " + result[ndx].Id);
        }

        [Test]
        public async Task TestInsert()
        {
            try
            {
                UOW.BeginTransaction();
                CategoryType catType = new CategoryType()
                {
                    Description = "TestInsertCategory",
                    Active = true
                };
                CategoryType result = await categoryTypeRepository.Insert(UOW, catType);
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
        public async Task TestUpdate()
        {
            try
            {
                UOW.BeginTransaction();
                CategoryType result = await categoryTypeRepository.GetByID(Context, 3);

                Assert.IsNotNull(result, "Expect to find CategoryType State");
                Assert.AreEqual(result.Description, "State", "Expect to find CategoryType Description = State");
                result.Description = "State Test Update";
                CategoryType updated = await categoryTypeRepository.Update(UOW, result);
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
        public async Task TestDelete()
        {
            try
            {
                UOW.BeginTransaction();
                CategoryType result = await categoryTypeRepository.GetByID(Context, 1);
                Assert.IsNotNull(result, "Expect to find CategoryType Measure");
                Assert.AreEqual(result.Description, "Measure", "Expect to find CategoryType Description = Measure");
                CategoryType deleted = await categoryTypeRepository.Delete(UOW, result.Id);
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
