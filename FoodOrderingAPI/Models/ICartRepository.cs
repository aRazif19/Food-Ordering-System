using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderingAPI.Models
{
    public interface ICartRepository
    {
        void AddToCart(Cart cart);
    }
}