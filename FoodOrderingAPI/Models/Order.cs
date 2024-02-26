using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodOrderingAPI.Models
{
    public class Order
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }

        [Required]
        public int Customer_ID { get; set; }
        [Required]
        public string Customer_name { get; set; }
        [Required]
        public string Customer_address { get; set;}
        [Required]
        
        [JsonIgnore]
        public decimal Total_price { get; set; }

        [ForeignKey("Customer_ID")]
        [JsonIgnore]
        public virtual Cart Cart { get; set; }

        [ForeignKey("FoodId")]
        [JsonIgnore]
        public virtual Food Food { get; set; }


    }
}