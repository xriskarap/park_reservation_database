namespace Capstone
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Capstone.DAL;
    using Capstone.Models;

    public class ReservationCLI
    {
        public const string DatabaseConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";

        private SiteSqlDAL siteDAL = new SiteSqlDAL(DatabaseConnectionString);

        private Campground campground;

        public ReservationCLI(Campground campground)
        {
            this.campground = campground;
            this.RunCLI();
        }

        private void RunCLI()
        {
            Console.WriteLine("What is the arrival date?");
            DateTime fromDate = this.GetDates();
        }

        private DateTime GetDates()
        {
            int year = 0;
            int month = 0;
            int day = 0;

            string userYear = string.Empty;

            bool isNumeric = false;

            Console.Write("Year: ");

            // While user year cannot be converted to a number,
            // or its length is less than 2,
            // or its length is greater than 4,
            // repeat the prompt
            while (userYear.Length < 2 || userYear.Length > 4 || isNumeric == false)
            {
                userYear = Console.ReadLine();

                if (userYear.Length == 2)
                {
                    userYear = "20" + userYear;
                }

                // isNumeric becomes true if userYear can be parsed as an int,
                // and if it is true, it spits out the value of userYear as year
                isNumeric = int.TryParse(userYear, out year);

                if (isNumeric == false)
                {
                    Console.WriteLine("Please enter a valid year.");
                    // THIS WON'T HAPPEN IF STRING IS TOO LONG BUT STILL NUMERIC;MUST REORDER CODE
                }
            }

            isNumeric = false;

            Console.Write("Month: ");
            string userMonth = Console.ReadLine();

            // While userMonth cannot be converted to an int,
            // or userMonth is not a valid numeric month (1-12),
            // prompt for input
            while (isNumeric == false || int.Parse(userMonth) > 12 || int.Parse(userMonth) < 1)
            {
                // isNumeric becomes true if userYear can be parsed as an int,
                // and if it is true, it spits out the value of userYear as year
                isNumeric = int.TryParse(userMonth, out month);

                if (isNumeric == false)
                {
                    Console.WriteLine("Please enter a valid month (numeric)");
                    userMonth = Console.ReadLine();

                    // Could build a switch case or dictionary to get numeric values from string
                }
            }

            Console.Write("Day: ");
            string userDay = Console.ReadLine();

            while (isNumeric == false || int.Parse(userDay) > 31 || int.Parse(userDay) < 1)
            {
                // isNumeric becomes true if userYear can be parsed as an int,
                // and if it is true, it spits out the value of userYear as year
                isNumeric = int.TryParse(userDay, out day);

                if (isNumeric == false)
                {
                    Console.WriteLine("Please enter a valid date");
                    userDay = Console.ReadLine();

                    // Could build a switch case or dictionary to get numeric values from string
                }
            }

            DateTime date = Convert.ToDateTime(year + "-" + month + "-" + day);

            Console.WriteLine(date);

            return date;
        }
    }
}
