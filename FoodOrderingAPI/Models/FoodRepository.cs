using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FoodOrderingAPI.Models
{
    public class FoodRepository : IFoodRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;
        List<Food> Foods = new List<Food>();
        public FoodRepository() 
        {
            using (SqlConnection Conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Food", Conn);

                Conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Food food = new Food()
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        Food_name = Convert.ToString(reader["Food_name"]),
                        Food_price = Convert.ToDecimal(reader["Food_Price"]),
                        Food_description = Convert.ToString(reader["Food_description"]),
                    };

                    Foods.Add(food);
                }

                Conn.Close();
            }
        }

        public List<Food> GetAll()
        {
            return Foods;
        }

        public Food GetFood(int id)
        {
            return Foods.Find(p => p.Id == id);
        }
    }
}