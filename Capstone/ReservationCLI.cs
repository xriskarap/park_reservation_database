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

        private Reservation reservation;
        private Campground campground;
        private IList<Site> sites;
        private int totalDays;

        public ReservationCLI(Campground campground, bool flag)
        {
            // If flag is false, return to Parks
            // (flag represents a user choice of 0 from parent menu)
            if (flag == false)
            {
                this.ReturnToMainMenu();
            }
            else
            {
                this.reservation = new Reservation();
                this.campground = campground;
                this.GetSites();
                this.SitesAvailable();
                Site site = this.GetSiteSelection();
                string name = this.GetUserNames();
                int confirmation = this.BookIt(site, name);
                this.PresentConfirmation(confirmation);
            }
        }

        private void PresentConfirmation(int confirmation)
        {
            Console.WriteLine($"Congratulations! Your confirmation is #{confirmation}.");
        }

        private int BookIt(Site site, string name)
        {
            ReservationSqlDAL reservationDAL = new ReservationSqlDAL(DatabaseConnectionString);
            return reservationDAL.MakeReservation(this.reservation);
        }

        private string GetUserNames()
        {
            Console.WriteLine("What name should the reservation be made under?");
            string name = Console.ReadLine();

            this.reservation.Name = name;

            return name;
        }

        private void SitesAvailable()
        {
            string number = "Site #";
            string occupancy = "Max Occupancy";
            string accessible = "Accessibility";
            string lengthRV = "Max RV Length";
            string utility = "Utility Access";
            string cost = "Total Cost";
            Console.WriteLine($"{number.PadRight(10)}{occupancy.PadRight(16)}{accessible.PadRight(16)}{lengthRV.PadRight(16)}{utility.PadRight(16)}{cost}");

            foreach (Site site in this.sites)
            {
                Console.WriteLine($"#{site.Site_Number.ToString().PadRight(10)}{site.Max_Occupancy.ToString().PadRight(16)}{site.Accessible.ToString().PadRight(16)}{site.Max_Rv_Length.ToString().PadRight(16)}{site.Utilities.ToString().PadRight(16)}{this.totalDays * this.campground.Daily_Fee:C}");
            }

            return;
        }

        private Site GetSiteSelection()
        {
            Console.WriteLine("Which site should be reserved? Select by #");

            string userSelection = Console.ReadLine();

            Site selectedSite = new Site();

            // Repeat prompt until we have a site ID
            while (selectedSite.Site_Id == 0 || int.TryParse(userSelection, out int selection) == false)
            {
                foreach (Site site in this.sites)
                {
                    if (site.Site_Number == int.Parse(userSelection))
                    {
                        selectedSite = site;
                    }
                }

                if (selectedSite.Site_Number != int.Parse(userSelection))
                {
                    Console.WriteLine("Please select a valid site by #");
                    userSelection = Console.ReadLine();
                }
            }

            this.reservation.Site_Id = selectedSite.Site_Id;

            return selectedSite;
        }

        private ParkMenuCLI ReturnToMainMenu()
        {
            Console.Clear();
            return new ParkMenuCLI();
        }

        private IList<Site> GetSites()
        {
            // New list every time method is called
            this.sites = new List<Site>();

            Console.WriteLine("What is your arrival date?");
            DateTime startDate = this.GetDates();
            Console.WriteLine();
            Console.WriteLine("What is your departure date?");
            DateTime endDate = this.GetDates();

            if (endDate < startDate || startDate < DateTime.Now)
            {
                return this.InvalidDate();
            }

            // Total days is difference between endDate and startDate
            this.totalDays = int.Parse((endDate - startDate).TotalDays.ToString());

            // Update reservation object
            this.reservation.From_Date = startDate;
            this.reservation.To_Date = endDate;

            this.sites = this.siteDAL.GetAvailableSites(startDate, endDate, this.campground.Campground_Id);

            if (this.sites.Count == 0)
            {
                Console.WriteLine("No sites available for your given dates :(");
                Console.WriteLine("Please make a new selection");
                Console.ReadLine();
                this.ReturnToMainMenu();
            }

            return this.sites;
        }

        /// <summary>
        /// This is an IList<Site> to allow it to return from GetSites()
        /// Notifies user of an invalid date selection, and returns back to GetSites()
        /// </summary>
        /// <returns>Its parent method</returns>
        private IList<Site> InvalidDate()
        {
            Console.WriteLine("Uh-oh! Looks like you've selected an invalid date range.");
            Console.WriteLine("Please try again");
            Console.WriteLine();

            return this.GetSites();
        }

        private DateTime GetDates()
        {
            this.totalDays = 0;

            Console.Write("Year: ");
            int year = this.GetYear();

            Console.Write("Month: ");
            int month = this.GetMonth();

            Console.Write("Day: ");
            int day = this.GetDay();

            DateTime date = Convert.ToDateTime(year + "-" + month + "-" + day);

            return date;
        }

        private int GetYear()
        {
            string userYear = Console.ReadLine();
            bool isNumeric = false;

            // While user year cannot be converted to a number,
            // or its length is less than 2,
            // or its length is greater than 4,
            // repeat the prompt
            while (userYear.Length < 2 || userYear.Length > 4 || isNumeric == false)
            {
                // isNumeric becomes true if userYear can be parsed as an int
                isNumeric = int.TryParse(userYear, out int year);

                if (isNumeric == true)
                {
                    if (userYear.Length == 2)
                    {
                        userYear = "20" + userYear;
                    }

                    if (int.Parse(userYear) < DateTime.Now.Year)
                    {
                        Console.WriteLine("Looks like you're living in the past.");
                        isNumeric = false;
                    }
                    else
                    {
                        return int.Parse(userYear);
                    }
                }

                Console.WriteLine("Please enter a valid year.");
                userYear = Console.ReadLine();
            }

            return int.Parse(userYear);
        }

        private int GetMonth()
        {
            string userMonth = Console.ReadLine();
            bool isNumeric = false;

            // While userMonth cannot be converted to an int,
            // or userMonth is not a valid numeric month (1-12),
            // prompt for input
            while (isNumeric == false || int.Parse(userMonth) > 12 || int.Parse(userMonth) < 1)
            {
                // isNumeric becomes true if userYear can be parsed as an int,
                // and if it is true, it spits out the value of userYear as year
                isNumeric = int.TryParse(userMonth, out int month);

                if (isNumeric == true && month <= 12 && month >= 1)
                {
                    return month;
                }

                Console.WriteLine("Please enter a valid month (numeric)");
                userMonth = Console.ReadLine();

                // Could build a switch case or dictionary to get numeric values from string
            }

            return int.Parse(userMonth);
        }

        private int GetDay()
        {
            string userDay = Console.ReadLine();
            bool isNumeric = false;

            while (isNumeric == false || int.Parse(userDay) > 31 || int.Parse(userDay) < 1)
            {
                // isNumeric becomes true if userYear can be parsed as an int,
                // and if it is true, it spits out the value of userYear as year
                isNumeric = int.TryParse(userDay, out int day);

                if (isNumeric == true)
                {
                    return day;
                }

                Console.WriteLine("Please enter a valid date");
                userDay = Console.ReadLine();

                // Could build a switch case or dictionary to get numeric values from string
            }

            return int.Parse(userDay);
        }
    }
}
