using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodOrderingAPI.Models
{
    public class Cart
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public int Customer_ID { get; set; }
        [Required]
        public int FoodId { get; set; }
        //[Required]
        //public int Quantity { get; set; }

        [ForeignKey("FoodId")]
        [JsonIgnore]
        public virtual Food food { get; set; }
        
    }
}