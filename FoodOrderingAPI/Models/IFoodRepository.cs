using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderingAPI.Models
{
    public interface IFoodRepository
    {
        List<Food> GetAll();
        Food GetFood(int id);
    }
}