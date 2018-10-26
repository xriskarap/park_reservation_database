using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class ParkCampgroundsMenu
    {
        public const string DatabaseConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";

        private IList<Campground> campground;


        private IList<Campground> GetAllCampgrounds(int park_Id)
        {
            ICampground dal = new CampgroundSqlDAL(DatabaseConnectionString);
            IList<Campground> campground = dal.GetAllCampgrounds(park_Id);

            return campground;
        }

        public ParkCampgroundsMenu(IList<Campground> campground)
        {
            this.campground = campground;
        }

        private void PrintCampground()
        {
            int selection = 0;
            foreach (Campground ground in this.campground)
            {
                selection++;
                Console.WriteLine($"{selection}) {this.campground}");

            }
        }

        public void RunCLI()
        {
            this.PrintCampground();
            this.PrintMenu();

            string input = Console.ReadLine().ToUpper();

            if (input == "Q")
            {
                return;
            }

            int campgroundSelection = int.Parse(input);

            Campground selectedCampground = this.campground[campgroundSelection - 1];
            Console.Clear();
            Console.WriteLine($"Select a Campground");

            Console.WriteLine();
        }

    private void PrintMenu()
        {
            Console.WriteLine("Select a Command");
            Console.WriteLine();
            Console.WriteLine("Search For Reservation");
            Console.WriteLine("Return to Previous Screen");
            Console.WriteLine();
            string returnCampground = Console.ReadLine();
        }
    }

}
