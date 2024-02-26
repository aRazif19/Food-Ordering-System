using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodOrderingAPI.Models;

namespace FoodOrderingAPI.Controllers
{
    public class FoodsController : ApiController
    {
        private readonly IFoodRepository _foodRepository;

        public FoodsController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        // GET: api/Foods
        public List<Food> Get()
        {
            return _foodRepository.GetAll();
        }

        // GET: api/Foods/5
        public Food Get(int id)
        {
            Food food = _foodRepository.GetFood(id); 
            if (food == null)
            {
                return null;
            }
            return food;
        }
    }
}