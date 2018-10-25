using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Capstone
{
    public class MainMenuCLI
    {

        const string Command_AllParks = "1";
        const string Command_MakeReservation = "2";
        const string Command_Quit = "q";
        const string DatabaseConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPCCampsite;Integrated Security=True";

        public void RunCLI()
        {
            PrintMenu();

            while (true)
            {
                string command = Console.ReadLine();

                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_AllParks:
                        GetAllParks();
                        break;

                    //case Command_MakeReservation:
                    //    MakeReservation();
                        break;

                    case Command_Quit:
                        Console.WriteLine("Thank you for using the project organizer");
                        return;

                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;

                }

                PrintMenu();
            }
        }

        private void GetAllParks()
        {
            IPark dal = new ParkSqlDAL(DatabaseConnectionString);
            IList<Park> parks = dal.GetAllParks();

            if (parks.Count > 0)
            {
                foreach (Park park in parks)
                {
                    Console.WriteLine(park.Name.ToString().PadRight(10) + park.Name.PadRight(40));
                }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("Main Menu Please type in a command");
            Console.WriteLine(" 1 - View all Parks");
            Console.WriteLine(" 2 - Make a Reservation");
            Console.WriteLine(" Q - Quit");
            Console.WriteLine();

        }
    }
}
