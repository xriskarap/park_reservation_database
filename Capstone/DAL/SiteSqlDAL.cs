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


        public IList<Site> GetAvailableSites(DateTime startDate, DateTime endDate, int siteId)
        {
            List<Site> availSites = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM site WHERE site_id = @site_id AND site_id NOT IN (SELECT site_id FROM[NPCampsite].[dbo].[reservation] WHERE(from_date BETWEEN @startDate AND @endDate) AND(to_date BETWEEN @startDate AND @endDate) AND site_id = @site_id)", conn);

                    cmd.Parameters.AddWithValue("@site_id", siteId);
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
