using FoodOrderingAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FoodOrderingAPI.Models
{
    public class CartRepository : ICartRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;

        public void AddToCart(Cart cart)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Order_Cart (Food_ID, Customer_ID) VALUES (@FoodId, @Customer_ID); SELECT SCOPE_IDENTITY();", conn))
                    {
                        cmd.Parameters.AddWithValue("@FoodId", cart.FoodId);
                        cmd.Parameters.AddWithValue("@Customer_ID", cart.Customer_ID);

                        int cartId = Convert.ToInt32(cmd.ExecuteScalar());
                        cart.Id = cartId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
    }
}