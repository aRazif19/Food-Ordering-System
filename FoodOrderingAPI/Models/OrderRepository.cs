using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FoodOrderingAPI.Models
{
    public class OrderRepository : IOrderRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;
        public void makeOrder(Order order)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if the cart exists for the provided Customer_ID
                    using (SqlCommand cartCmd = new SqlCommand("SELECT COUNT(*) FROM Order_Cart WHERE Customer_ID = @Customer_ID AND Order_status IS NULL", conn))
                    {
                        cartCmd.Parameters.AddWithValue("@Customer_ID", order.Customer_ID);
                        int cartCount = Convert.ToInt32(cartCmd.ExecuteScalar());

                        if (cartCount == 0)
                        {
                            throw new Exception("No cart found for the provided Customer_ID.");
                        }
                    }

                    // Check if the food item exists in the cart
                    using (SqlCommand cartFoodCmd = new SqlCommand("SELECT COUNT(*) FROM Order_Cart WHERE Food_ID = @FoodId AND Order_status IS NULL", conn))
                    {
                        cartFoodCmd.Parameters.AddWithValue("@FoodId", order.FoodId);
                        int cartFoodCount = Convert.ToInt32(cartFoodCmd.ExecuteScalar());

                        if (cartFoodCount == 0)
                        {
                            throw new Exception($"Food item with ID {order.FoodId} is not in the cart.");
                        }
                    }

                    // Calculate total price
                    using (SqlCommand foodpriceCmd = new SqlCommand("SELECT Food_price FROM Food WHERE Id = @FoodId", conn))
                    {
                        foodpriceCmd.Parameters.AddWithValue("@FoodId", order.FoodId);
                        decimal foodPrice = Convert.ToDecimal(foodpriceCmd.ExecuteScalar());

                        order.Total_price = order.Quantity * foodPrice;
                    }

                    // Insert the order
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Order_Cart (Quantity, Total_price, Customer_name, Customer_address, Order_status, Customer_ID, Food_ID) VALUES (@Quantity, @Total_price, @Customer_name, @Customer_address, @Order_status, @Customer_ID, @FoodId); SELECT SCOPE_IDENTITY();", conn))
                    {
                        cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
                        cmd.Parameters.AddWithValue("@Total_price", order.Total_price);
                        cmd.Parameters.AddWithValue("@Customer_name", order.Customer_name);
                        cmd.Parameters.AddWithValue("@Customer_address", order.Customer_address);
                        cmd.Parameters.AddWithValue("@Order_status", "Completed");
                        cmd.Parameters.AddWithValue("@Customer_ID", order.Customer_ID);
                        cmd.Parameters.AddWithValue("@FoodId", order.FoodId);

                        int orderId = Convert.ToInt32(cmd.ExecuteScalar());
                        order.Id = orderId;
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