using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Capstone.Models;

namespace Capstone.DAL
{
    public class ReservationSqlDAL : IReservation
    {
        private string connectionString;

        public ReservationSqlDAL(string dbConnectionString)
        {
            this.connectionString = dbConnectionString;
        }

//        SELECT* FROM

//    site
//     WHERE site_id NOT IN
//(SELECT site_id
//  FROM[NPCampsite].[dbo].[reservation]
//  WHERE (from_date BETWEEN '2018-10-21' AND '2018-10-28')
//  AND(to_date BETWEEN '2018-10-21' AND '2018-10-28'))

        public IList<Reservation> MakeReservation(Reservation newReservation)
        {
            List<Reservation> reservation = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO reservation (site_id, name, from_date, to_date, create_date) VALUES (@site_id, @name, @from_date, @to_date, @create_date)", conn);

                    cmd.Parameters.AddWithValue("@site_id", newReservation.Site_Id);
                    cmd.Parameters.AddWithValue("@name", newReservation.Name);
                    cmd.Parameters.AddWithValue("@from_date", newReservation.From_Date);
                    cmd.Parameters.AddWithValue("@to_date", newReservation.To_Date);
                    cmd.Parameters.AddWithValue("@create_date", newReservation.Create_Date);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error reading data.");
                throw;
            }

            return reservation;
        }
    }
}
