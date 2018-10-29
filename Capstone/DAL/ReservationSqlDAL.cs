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

            /// <summary>
            /// Allows a user to make a reservation by inserting the user input
            /// into the database
            /// </summary>
            /// <param name="newReservation">User's new Reservation</param>
            /// <returns>A new Reservation</returns>
        public int MakeReservation(Reservation newReservation)
        {
            int confirmationNumber = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO reservation (site_id, name, from_date, to_date) VALUES (@site_id, @name, @from_date, @to_date); SELECT SCOPE_IDENTITY();", conn);

                    cmd.Parameters.AddWithValue("@site_id", newReservation.Site_Id);
                    cmd.Parameters.AddWithValue("@name", newReservation.Name);
                    cmd.Parameters.AddWithValue("@from_date", newReservation.From_Date.ToString("MM/dd/yyyy"));
                    cmd.Parameters.AddWithValue("@to_date", newReservation.To_Date.ToString("MM/dd/yyyy"));

                    conn.Open();

                    confirmationNumber = (int)(decimal)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return confirmationNumber;
        }

        private static int GetConfirmation (SqlDataReader reader)
        {
            int confirmationNum;
            confirmationNum = Convert.ToInt32(reader["reservation_id"]);
            return confirmationNum;
        }
    }
}
