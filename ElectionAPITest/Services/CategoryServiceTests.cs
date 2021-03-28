using ElectionAPI.Service;
using ElectionModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionAPITest.Services
{
    [TestFixture]
    public class CategoryServiceTests : BaseServiceTests
    {
        private CategoryService categoryService = new CategoryService();
      
        [SetUp]
        public override void Setup()
        {
            base.Setup();
        }

        [Test]
        public async Task TestGetByElection()
        {
            try
            {
                IEnumerable<Category> result = await categoryService.GetByElection(Context, this.DefaultElectionId);
                Assert.IsNotNull(result, "Expect to find at least once category in default dataset");
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
                List<Category> ans = (await categoryService.GetByElection(Context, this.DefaultElectionId)).ToList();
                Assert.IsNotNull(ans, "There are at least 142 categories in default dataset");
                int testIndex = rand.Next(0, ans.Count);
                Category testCategory = ans[testIndex];
                Category result = await categoryService.GetByID(Context, testCategory.Id);
                Assert.IsNotNull(result, "Expect to " + testCategory.Title);
                Assert.AreEqual(result.Title, testCategory.Title, "Expect title to be " + testCategory.Title + " found " + result.Title);
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }

        [Test]
        public async Task TestGetByType()
        {
            try
            {
                int testType = rand.Next(1, 5);
                List<Category> ans = (await categoryService.GetByType(Context, this.DefaultElectionId, testType)).ToList();
                Assert.IsNotNull(ans, "Expect at least on categorytype " + testType + " in default dataset");
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
                Category insCat = new Category()
                {
                    ElectionId = this.DefaultElectionId,
                    CategoryTypeId = CategoryTypeEnum.federal,
                    Heading = "TestHeadingInsert",
                    Title = "TestTitleInsert",
                    JudgePosition = 4,
                    Information = "TestInformationInsert",
                    SubTitle = "TestSubTitleInsert",
                    Sequence = 1,
                    Selection = null
                };
                UOW.BeginTransaction();
                Category result = await categoryService.Insert(UOW, insCat);
                UOW.CloseTransaction();
                Assert.IsNotNull(result, "Expect category to be inserted");
                Assert.IsFalse(result.Id == Guid.Empty, "Expect a real guid id for inserted Category");
                Assert.AreEqual(result.CategoryTypeId, insCat.CategoryTypeId, "Expect CategoryTypeId to be the same");
                Assert.AreEqual(result.Heading, insCat.Heading, "Expect Heading to be the same");
                Assert.AreEqual(result.Title, insCat.Title, "Expect Title to be the same");
                Assert.AreEqual(result.JudgePosition, insCat.JudgePosition, "Expect JudgePosition to be the same");
                Assert.AreEqual(result.Information, insCat.Information, "Expect Information to be the same");
                Assert.AreEqual(result.SubTitle, insCat.SubTitle, "Expect SubTitle to be the same");
                Assert.AreEqual(result.Sequence, insCat.Sequence, "Expect Sequence to be the same");
                Assert.AreEqual(result.Selection, insCat.Selection, "Expect Selection to be the same");
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
                Category insCat = new Category()
                {
                    ElectionId = this.DefaultElectionId,
                    CategoryTypeId = CategoryTypeEnum.federal,
                    Heading = "TestHeadingInsert",
                    Title = "TestTitleInsert",
                    JudgePosition = 4,
                    Information = "TestInformationInsert",
                    SubTitle = "TestSubTitleInsert",
                    Sequence = 1,
                    Selection = null
                };
                UOW.BeginTransaction();
                Category result = await categoryService.Insert(UOW, insCat);
                Assert.IsNotNull(result, "Expect category to be inserted");
                Assert.IsFalse(result.Id == Guid.Empty, "Expect a real guid id for inserted Category");
                Assert.AreEqual(result.CategoryTypeId, insCat.CategoryTypeId, "Expect CategoryTypeId to be the same");
                Assert.AreEqual(result.Heading, insCat.Heading, "Expect Heading to be the same");
                Assert.AreEqual(result.Title, insCat.Title, "Expect Title to be the same");
                Assert.AreEqual(result.JudgePosition, insCat.JudgePosition, "Expect JudgePosition to be the same");
                Assert.AreEqual(result.Information, insCat.Information, "Expect Information to be the same");
                Assert.AreEqual(result.SubTitle, insCat.SubTitle, "Expect SubTitle to be the same");
                Assert.AreEqual(result.Sequence, insCat.Sequence, "Expect Sequence to be the same");
                Assert.AreEqual(result.Selection, insCat.Selection, "Expect Selection to be the same");

                result.Heading = "TestHeadingUpdate";
                result.Title = "TestTitleUdate";
                result.Information = "TestInformationUpdate";
                result.SubTitle = "TestSubTitleUpdate";
                Category update = await categoryService.Update(UOW, result);
                UOW.CloseTransaction();
                Assert.IsNotNull(update, "Expected updated record");
                Assert.IsTrue(update.Heading == result.Heading, "Expect Heading to be updated");
                Assert.IsTrue(update.Title == result.Title, "Expect Title to be updated");
                Assert.IsTrue(update.Information == result.Information, "Expect Information to be updated");
                Assert.IsTrue(update.SubTitle == result.SubTitle, "Expect Subtitle to be updated");
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
                List<Category> result = (await categoryService.GetByElection(Context, this.DefaultElectionId)).ToList();
                int total = result.Count;
                Assert.IsNotNull(result, "Expect to find at least once category in default dataset");
                int ndxToDelete = rand.Next(1, total);
                Category catToDelete = result[ndxToDelete];
                UOW.BeginTransaction();
                Category deleted = (await categoryService.Delete(UOW, catToDelete.Id));
                List<Category> result2 = (await categoryService.GetByElection(Context, this.DefaultElectionId)).ToList();
                UOW.CloseTransaction();
                Assert.IsNull(deleted, "Expected Category to be deleted");
                Assert.IsTrue(result2.Count == total - 1, "Expect one less category in default dataset");
            }
            catch (Exception ex)
            {
                Assert.IsNull(ex, "Exception Thrown: " + ex.Message);
            }
        }
    }
}
