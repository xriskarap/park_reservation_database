namespace Capstone
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Capstone.DAL;
    using Capstone.Models;

    public class ParkCampgroundsMenu
    {
        private IList<Campground> campground;
        private Dictionary<int, string> months = new Dictionary<int, string>()
        {
            { 1, "January" },
            { 2, "February" },
            { 3, "March" },
            { 4, "April" },
            { 5, "May" },
            { 6, "June" },
            { 7, "July" },
            { 8, "August" },
            { 9, "September" },
            { 10, "October" },
            { 11, "November" },
            { 12, "December" }
        };

        public ParkCampgroundsMenu(IList<Campground> campground)
        {
            this.campground = campground;
        }

        // Gives meaning to your cases
        const string Command_ViewAllCampgrounds = "1";
        const string Command_SearchForReservation = "2";
        const string Command_ReturnToPreviousMenu = "3";
        public const string DatabaseConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";

        public void RunCLI()
        {
            this.PrintMenu();

            while (true)
            {
                string command = CLIHelper.GetString("Make a choice: ");

                switch (command.ToLower())
                {
                    case Command_ViewAllCampgrounds:
                        // go through the list of campgrounds and display
                        this.PrintCampground();
                        break;

                    // case Command_ReturnToPreviousMenu:
                    //    ParkMenuCLI parkMenu = new ParkMenuCLI(GetAllParks());
                    //    parkMenu.RunCLI();
                    //    break;
                    case Command_ReturnToPreviousMenu:
                        Console.Clear();
                        new ParkMenuCLI();
                        return;
                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;
                }
            }
        }

        public void ListAllCampgrounds()
        {
            this.PrintMenu();
            this.PrintCampground();

            string input = Console.ReadLine().ToUpper();

            if (input == "Q")
            {
                return;
            }

            int campgroundSelection = int.Parse(input);
            Campground selectedCampground = this.campground[campgroundSelection - 1];
            Console.Clear();
            Console.WriteLine($"Select a Campground");
            Console.WriteLine($"{selectedCampground.Name}");
            Console.WriteLine($"{selectedCampground.Open_From_Mm}");
            Console.WriteLine();
        }

        public void PrintMenu()
        {
            Console.WriteLine("Select a Command");
            Console.WriteLine();
            Console.WriteLine("1) View All Campgrounds");
            Console.WriteLine("2) Search For Reservation");
            Console.WriteLine("3) Return to Previous Screen");
            Console.WriteLine();
        }

        private IList<Campground> GetAllCampgrounds(int park_Id)
        {
            ICampground dal = new CampgroundSqlDAL(DatabaseConnectionString);
            IList<Campground> campground = dal.GetAllCampgrounds(park_Id);

            return campground;
        }

        private ReservationCLI PrintCampground()
        {
            int selection = 0;
            string name = "Name";
            string open = "Open";
            string close = "Close";
            string fee = "Daily Fee";
            Console.WriteLine($"   {name.PadRight(34)}{open.PadRight(16)}{close.PadRight(16)}{fee}");
            foreach (Campground ground in this.campground)
            {
                selection++;
                Console.WriteLine($"#{selection} {ground.Name.PadRight(34)} {ground.Open_From_Mm.ToString().PadRight(15)} {ground.Open_To_Mm.ToString().PadRight(15)} {ground.Daily_Fee:C}");
            }

            return this.GetUserSelection();
        }

        private ReservationCLI GetUserSelection()
        {
            Campground campground = new Campground();

            Console.WriteLine();
            Console.WriteLine("Which campground (enter 0 to cancel)");

            int userSelection = int.Parse(Console.ReadLine());

            while (userSelection > this.campground.Count)
            {
                Console.WriteLine("Sorry, that's not a valid selection.");
                Console.WriteLine("Please make another selection.");
                userSelection = int.Parse(Console.ReadLine());
            }

            campground = this.campground[userSelection - 1];

            ReservationCLI reservationCLI = new ReservationCLI(campground);

            return reservationCLI;
        }
    }
}