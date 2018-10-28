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

        private IList<Park> parks;

        public ParkMenuCLI(IList<Park> parks)
        {
            this.parks = parks;
        }

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
            Console.WriteLine($"{selectedPark.Location}");
            Console.WriteLine($"{selectedPark.Establish_Date}");
            Console.WriteLine($"{selectedPark.Area}");
            Console.WriteLine($"{selectedPark.Visitors}");
            Console.WriteLine();
            Console.WriteLine($"{selectedPark.Description}");
            Console.WriteLine();
            ICampground dal = new CampgroundSqlDAL(DatabaseConnectionString);
            IList<Campground> campgrounds = dal.GetAllCampgrounds(selectedPark.Park_Id);
            ParkCampgroundsMenu campgroundsMenu = new ParkCampgroundsMenu(campgrounds);
            campgroundsMenu.RunCLI();
        }

    private void PrintParks()
        {
            int selection = 0;
            foreach (Park park in this.parks)
            {
                selection++;
                Console.WriteLine($"{selection}) {park.Name}");
            }
        }

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
