using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class SiteSqlDAL : ISite
    {
        private string connectionString;

        public SiteSqlDAL(string dbConnectionString)
        {
            this.connectionString = dbConnectionString;
        }

        public IList<Site> Search_Site_Number(int campground_Id, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Queries the database to select all Sites within a Campground
        /// </summary>
        /// <param name="startDate">Start date for a Reservation</param>
        /// <param name="endDate">End date for a Reservation</param>
        /// <param name="siteId">The id associated with an individual site</param>
        /// <returns>A list of all Sites</returns>
        public IList<Site> GetAvailableSites(DateTime startDate, DateTime endDate, int campgroundId)
        {
            List<Site> availSites = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT TOP 5 * FROM site WHERE campground_id = @campgroundId AND site_id IN (SELECT site_id FROM[NPCampsite].[dbo].[reservation] WHERE NOT (from_date BETWEEN @startDate AND @endDate) AND NOT (to_date BETWEEN @startDate AND @endDate))", conn);

                    cmd.Parameters.AddWithValue("@campgroundId", campgroundId);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = ConvertRowToSite(reader);
                        availSites.Add(site);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return availSites;
        }

        private static Site ConvertRowToSite(SqlDataReader reader)
        {
            Site site = new Site();
            site.Accessible = Convert.ToBoolean(reader["accessible"]);
            site.Campground_Id = Convert.ToInt32(reader["campground_id"]);
            site.Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]);
            site.Max_Rv_Length = Convert.ToInt32(reader["max_rv_length"]);
            site.Site_Id = Convert.ToInt32(reader["site_id"]);
            site.Site_Number = Convert.ToInt32(reader["site_number"]);
            site.Utilities = Convert.ToBoolean(reader["utilities"]);
            return site;
        }
    }
}
