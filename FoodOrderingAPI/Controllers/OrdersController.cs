using FoodOrderingAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodOrderingAPI.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrderRepository _orderRepository;

        // GET: api/Orders
        public OrdersController(IOrderRepository orderRepository) 
        {
            _orderRepository = orderRepository;
        }

        // POST: api/Orders
        public IHttpActionResult Post(Order order)
        {
            try
            {
                _orderRepository.makeOrder(order);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
