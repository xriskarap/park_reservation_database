using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Capstone
{
    public class MainMenuCLI
    {
        //Gives meaning to your cases
        const string Command_AllParks = "1";
        const string Command_MakeReservation = "2";
        const string Command_Quit = "q";
        const string DatabaseConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPCampsite;Integrated Security=True";

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
                        ParkMenuCLI parkMenu = new ParkMenuCLI(this.GetAllParks());
                        parkMenu.RunCLI();
                        break;

                    case Command_Quit:
                        Console.WriteLine("Thank you for using the Reservation Application.");
                        return;

                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;

                }
            }
        }

        private IList<Park> GetAllParks()
        {
            IPark dal = new ParkSqlDAL(DatabaseConnectionString);
            IList<Park> parks = dal.GetAllParks();

            return parks;
        }

        private void PrintMenu()
        {
            Console.WriteLine("Main Menu Please type in a command");
            Console.WriteLine(" 1) - View all Parks");
            Console.WriteLine(" Q) - Quit");
            Console.WriteLine();

        }
    }
}
