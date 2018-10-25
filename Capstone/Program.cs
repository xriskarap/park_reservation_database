using Capstone;
using System;

namespace capstone
{
    public class Program
    {
        static void Main(string[] args)
        {
            MainMenuCLI cli = new MainMenuCLI();
            cli.RunCLI();
        }
    }
}
