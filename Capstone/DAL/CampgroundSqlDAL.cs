using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampgroundSqlDAL : ICampground
    {
        private string connectionString;

        public CampgroundSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Campground> GetAllCampgrounds(int park_Id)
        {
            List<Campground> output = new List<Campground>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground INNER JOIN park ON campground.park_id = park.park_id;");

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground campground = new Campground();
                        campground.Campground_Id = Convert.ToInt32(reader["campground_id"]);
                        campground.Park_Id = Convert.ToInt32(reader["park_id"]);
                        campground.Name = Convert.ToString(reader["name"]); ;
                        campground.Open_From_Mm = Convert.ToDateTime(reader["open_from_mm"]);
                        campground.Open_To_Mm = Convert.ToDateTime(reader["open_to_mm"]);
                        campground.Daily_Fee = Convert.ToDecimal(reader["daily_fee"]);

                        output.Add(campground);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error reading Campground data.");
                throw;
            }
            return output;
        }
        
    }
}
