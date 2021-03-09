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
using Newtonsoft.Json;

namespace ElectionAPITest.CategoryControllerTests
{
    [TestFixture]
    public class CategoryControllerTest : BaseControllerTest
    {
        Mock<ICategoryRepository> mockICategoryRepository;
        CategoryController categoryController;
        List<Category> mockCategoryList;
        Guid electionId;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            electionId = Guid.Parse("a13acd4a-d415-4b27-afe6-e2310ac71bc6");
            mockICategoryRepository = new Mock<ICategoryRepository>();
            categoryController = new CategoryController(mockIConfiguration.Object, mockICategoryRepository.Object);
            mockCategoryList = JsonConvert.DeserializeObject<List<Category>>(testResource.testCategories);
        }

        [Test]
        public async Task CategoryControllerGetByElectionId()
        {

            mockICategoryRepository.Setup(r => r.GetByElection(It.IsAny<IDbConnection>(), electionId)).Returns(Task.FromResult(mockCategoryList));
            List<Category> categoryList = await categoryController.GetByElection(electionId);
            Assert.AreEqual(categoryList, mockCategoryList);
        }

        [Test]
        public async Task CategoryControllerGetById()
        {
            Guid testId = Guid.Parse("97fe1731-9e87-47ce-b054-fb6a350e8799");
            mockICategoryRepository.Setup(r => r.GetByID(It.IsAny<IDbConnection>(), testId))
                .Returns(Task.FromResult(mockCategoryList.SingleOrDefault(n => n.Id == testId)));
            Category category = await categoryController.GetById(testId);
            Assert.AreEqual(category, mockCategoryList.SingleOrDefault(n => n.Id == testId));
        }

        [Test]
        public async Task CategoryControllerGetByCategoryType()
        {
            CategoryTypeEnum testCategoryType = CategoryTypeEnum.judicial;
            mockICategoryRepository.Setup(r => r.GetByType(It.IsAny<IDbConnection>(), electionId, (int)testCategoryType))
                .Returns(Task.FromResult(mockCategoryList.Where(n => n.CategoryTypeId == testCategoryType && n.ElectionId == electionId).ToList()));
            List<Category> categorylist = await categoryController.GetByCategoryType(electionId, (int)testCategoryType);
            Assert.AreEqual(categorylist, mockCategoryList.Where(n => n.CategoryTypeId == testCategoryType && n.ElectionId == electionId).ToList());
        }

        [Test]
        public async Task CategoryControllerUpdate()
        {
            Guid testId = Guid.Parse("383f37d2-4613-4cf2-a83c-7e004251ca2f");
            Category updateCategory = mockCategoryList.SingleOrDefault(n => n.Id == testId);
            string initialValue = updateCategory.SubTitle;
            updateCategory.SubTitle = initialValue + "-updateText";

            mockICategoryRepository.Setup(r => r.Update(It.IsAny<IDbConnection>(), updateCategory))
                .Returns(Task.FromResult(mockCategoryList.SingleOrDefault(n => n.Id == testId)));

            Category result = await categoryController.Update(updateCategory);
            Assert.AreEqual(result, mockCategoryList.SingleOrDefault(n => n.Id == testId));
        }
    }
}
