using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using FoodOrderingAPI.Models;
using FoodOrderingAPI.Controllers;
using System.Web.Http.Results;

namespace FoodOrderingAPI.Tests.Controllers
{
    [TestClass]
    public class CartsControllerTests
    {
        [TestMethod]
        public void Post_ShouldReturnOkResult()
        {
            //Arrange
            var mockCartRepository = new Mock<ICartRepository>();
            var controller = new CartsController(mockCartRepository.Object);
            var cart = new Cart
            {
                Id = 5,
                Customer_ID = 25,
                FoodId = 3,
            };

            //Act
            var result = controller.Post(cart) as OkNegotiatedContentResult<Cart>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(3, result.Content.FoodId);
        }

        [TestMethod]
        public void Post_ShouldReturnInternalServerErrorResult() 
        {
            //Arrange
            var mockCartRepository = new Mock<ICartRepository>();
            mockCartRepository.Setup(r => r.AddToCart(It.IsAny<Cart>())).Throws(new Exception("Test Exception"));
            var controller = new CartsController(mockCartRepository.Object);
            var cart = new Cart
            {
                Id = 5,
                Customer_ID = 25,
                FoodId = 3,
            };

            //Act
            var result = controller.Post(cart);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ExceptionResult));
        }
    }
}
