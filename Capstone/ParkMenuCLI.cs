namespace Capstone
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Capstone.DAL;
    using Capstone.Models;

    public class ParkMenuCLI
    {
        public const string DatabaseConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";

        private ParkSqlDAL parkDAL = new ParkSqlDAL(DatabaseConnectionString);

        private IList<Park> parks;

        // Whenever ParkMenuCLI is instantiated, it immediately builds a list of parks
        // and runs the command line interface
        public ParkMenuCLI()
        {
            this.parks = this.parkDAL.GetAllParks();
            this.RunCLI();
        }

        /// <summary>
        /// Allows the user to view all of the information for any given park
        /// If the user presses "Q" or "q", they will be brought to the previous menu
        /// </summary>
        public void RunCLI()
        {
            this.PrintMenu();
            string input = Console.ReadLine().ToUpper();

            if (input == "Q")
            {
                return;
            }

            int parkSelection = int.Parse(input);

            Park selectedPark = this.parks[parkSelection - 1];
            Console.Clear();
            Console.WriteLine($"Park Information Screen");
            Console.WriteLine($"{selectedPark.Name}");
            Console.WriteLine($"Location: {selectedPark.Location}");
            Console.WriteLine($"Established: {selectedPark.Establish_Date.ToShortDateString()}");
            Console.WriteLine($"Area: {selectedPark.Area} sq km");
            Console.WriteLine($"Annual Visitors: {selectedPark.Visitors}");
            Console.WriteLine();
            Console.WriteLine($"{selectedPark.Description}");
            Console.WriteLine();
            ICampground dal = new CampgroundSqlDAL(DatabaseConnectionString);
            IList<Campground> campgrounds = dal.GetAllCampgrounds(selectedPark.Park_Id);
            ParkCampgroundsMenu campgroundsMenu = new ParkCampgroundsMenu(campgrounds);
            campgroundsMenu.RunCLI();
            return;
        }

        /// <summary>
        /// Displays all parks with the corresponding selection number for the user
        /// </summary>
    private void PrintParks()
        {
            int selection = 0;
            foreach (Park park in this.parks)
            {
                selection++;
                Console.WriteLine($"{selection}) {park.Name}");
            }
        }

        /// <summary>
        /// Prints the menu allowing users to select a specific park
        /// </summary>
        private void PrintMenu()
        {
            Console.WriteLine("Select a Park for Further Details");
            Console.WriteLine();
            this.PrintParks();
            Console.WriteLine("Q) - Quit");
            Console.WriteLine();
        }
    }
}
