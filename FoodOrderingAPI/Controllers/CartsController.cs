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
    public class CartsController : ApiController
    {

        private readonly ICartRepository _cartRepository;

        public CartsController() { }

        public CartsController(ICartRepository cartRepository) 
        {
            _cartRepository = cartRepository;
        }

        // POST: api/Carts
        public IHttpActionResult Post(Cart val)
        {
            try
            {
                _cartRepository.AddToCart(val);
                return Ok(val);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
