using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodOrderingAPI.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        public string Food_name { get; set; }
        public decimal Food_price { get; set; }
        public string Food_description { get; set; }
    }
}