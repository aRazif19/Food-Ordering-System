using System;
using System.Web.Http.Results;
using FoodOrderingAPI.Controllers;
using FoodOrderingAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FoodOrderingAPI.Tests.Controllers
{
    [TestClass]
    public class OrdersControllerTests
    {
        [TestMethod]
        public void Post_ShouldReturnOkResult()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            var controller = new OrdersController(mockOrderRepository.Object);
            var order = new Order
            {
                Id = 1,
                Customer_ID = 1001,
                FoodId = 4,
                Quantity = 2,
                Customer_name = "John Doe",
                Customer_address = "123 Main St, City, Country"
            };

            // Act
            var result = controller.Post(order) as OkNegotiatedContentResult<Order>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(1001, result.Content.Customer_ID);
            Assert.AreEqual(4, result.Content.FoodId);
        }

        [TestMethod]
        public void Post_ShouldReturnInternalServerErrorResult()
        {
            // Arrange
            var mockOrderRepository = new Mock<IOrderRepository>();
            mockOrderRepository.Setup(r => r.makeOrder(It.IsAny<Order>())).Throws(new Exception("Test Exception"));
            var controller = new OrdersController(mockOrderRepository.Object);
            var order = new Order
            {
                Id = 1,
                Customer_ID = 1001,
                FoodId = 4,
                Quantity = 2,
                Customer_name = "John Doe",
                Customer_address = "123 Main St, City, Country"
            };

            // Act
            var result = controller.Post(order);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ExceptionResult));
        }
    }
}
