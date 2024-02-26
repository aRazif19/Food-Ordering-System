using System;
using System.Collections.Generic;
using System.Web.Http;
using FoodOrderingAPI.Controllers;
using FoodOrderingAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FoodOrderingAPI.Tests.Controllers
{
    [TestClass]
    public class FoodsControllerTests
    {
        [TestMethod]
        public void GetAllFoods_ShouldReturnAllFoods()
        {
            // Arrange
            var mockRepository = new Mock<IFoodRepository>();
            mockRepository.Setup(repo => repo.GetAll()).Returns(GetFoodList());
            var controller = new FoodsController(mockRepository.Object);

            // Act
            List<Food> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count); 
        }

        [TestMethod]
        public void GetFoodByID_ShouldReturnFoodByID()
        {
            // Arrange
            var mockRepository = new Mock<IFoodRepository>();
            mockRepository.Setup(repo => repo.GetFood(It.IsAny<int>())).Returns((int id) =>
            {
                if (id == 1)
                {
                    return new Food
                    {
                        Id = 1,
                        Food_name = "Test1",
                        Food_price = 10.00M,
                        Food_description = "This description for test1"
                    };
                }
                else
                {
                    return null; 
                }
            });

            var controller = new FoodsController(mockRepository.Object);
            int existingFoodId = 1;

            // Act
            var result = controller.Get(existingFoodId);

            // Assert
            Assert.IsNotNull(result);
            var contentResult = result as Food;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(existingFoodId, contentResult.Id);
            Assert.AreEqual("Test1", contentResult.Food_name);
            Assert.AreEqual(10.00M, contentResult.Food_price);
            Assert.AreEqual("This description for test1", contentResult.Food_description);
        }


        [TestMethod]
        public void GetFoodByNonExistingID_ShouldReturnNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IFoodRepository>();
            mockRepository.Setup(repo => repo.GetFood(It.IsAny<int>())).Returns((int id) => null);

            var controller = new FoodsController(mockRepository.Object);
            int nonExistingFoodId = -1;

            // Act
            var result = controller.Get(nonExistingFoodId);

            // Assert
            Assert.IsNull(result);
        }


        private List<Food> GetFoodList()
        {
            var testFood = new List<Food>();
            testFood.Add(new Food
            {
                Id = 1,
                Food_name = "Test1",
                Food_price = 10.00M,
                Food_description = "This description for test1"
            });
            testFood.Add(new Food
            {
                Id = 2,
                Food_name = "Test2",
                Food_price = 15.00M,
                Food_description = "This description for test2"
            });
            testFood.Add(new Food
            {
                Id = 3,
                Food_name = "Test3",
                Food_price = 17.00M,
                Food_description = "This description for test3"
            });
            testFood.Add(new Food
            {
                Id = 4,
                Food_name = "Test4",
                Food_price = 20.00M,
                Food_description = "This description for test4"
            });

            return testFood;
        }
    }
}
