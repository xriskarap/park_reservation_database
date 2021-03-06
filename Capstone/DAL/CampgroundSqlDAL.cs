﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class CampgroundSqlDAL : ICampground
    {
        // private const string getCampgrounds = @"SELECT * FROM campground WHERE park_id = @parkID ORDER BY name;";
        private string connectionString;

        public CampgroundSqlDAL(string dbConnectionString)
        {
            this.connectionString = dbConnectionString;
        }

        /// <summary>
        /// Queries the database to return a list of all Campgrounds within a particular Park
        /// </summary>
        /// <param name="park_Id">Park containing the Campground</param>
        /// <returns>A list of Campgrounds</returns>
        public IList<Campground> GetAllCampgrounds(int park_Id)
        {
            List<Campground> output = new List<Campground>();
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE park_id = @park_Id;", conn);
                    cmd.Parameters.AddWithValue("park_Id", park_Id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground campground = ConvertRowToCampground(reader);
                        output.Add(campground);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return output;
        }

        private static Campground ConvertRowToCampground(SqlDataReader reader)
        {
            Campground campground = new Campground();
            campground.Campground_Id = Convert.ToInt32(reader["campground_id"]);
            campground.Park_Id = Convert.ToInt32(reader["park_id"]);
            campground.Name = Convert.ToString(reader["name"]);
            campground.Open_From_Mm = Convert.ToInt32(reader["open_from_mm"]);
            campground.Open_To_Mm = Convert.ToInt32(reader["open_to_mm"]);
            campground.Daily_Fee = Convert.ToDecimal(reader["daily_fee"]);
            return campground;
        }
    }
}
