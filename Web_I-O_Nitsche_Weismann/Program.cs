using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop_Nitsche_Weismann;

namespace Web_I_O_Nitsche_Weismann
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string filePathHealthFitness = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/HealthFitnessApps.CSV";
            string filePathPhotpgraphy = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/PhotographyApps.CSV";
            string filePathWeather = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/WeatherApps.CSV";

            DataLoader.ReadAppsFromURL(';', filePathHealthFitness);
            DataLoader.ReadAppsFromURL(';', filePathPhotpgraphy);
            DataLoader.ReadAppsFromURL(';', filePathWeather);

            

            DataLoader.FilterApps(myEnums.Filter.Size, myEnums.Operator.greater_or_equals, 2);

            Mainmenu();

            //DisplayAppsFromListToConsole(DataLoader.FilteredApps);

            //DisplayAppsFromListToConsole(DataLoader.AllApps);

        }

        #region Menu
        public static void Mainmenu()
        {
            // Main Menu
            bool exit = false;
            ConsoleMenu Mainmenu = new ConsoleMenu(new Option[]{
                new Option("Show all Apps", () => DisplayAppsFromListToConsole(DataLoader.AllApps)),
                new Option("Filter Apps", () => SubmenuFilterApps()),
                new Option("Show lines where conversion didn't work", () => DisplayErrorLines(DataLoader.ErrorLines)),
                new Option("Exit", () => exit = true)
            });

            //Main Loop
            do
            {
                Console.Clear();
                Console.WriteLine("Navigate with Arrow Up - Arrow Down - Keys\n");
                Mainmenu.MenuLoopInConsole();
            } while (!exit);
        }
        public static void SubmenuFilterApps()
        {
            Console.Clear();
            Console.WriteLine("What will you filter\n");

            ConsoleMenu ChangeUserMenu = new ConsoleMenu(new Option[]{
                new Option("Price", () => SubmenuFilterIsPrice()),
                new Option("Reviews", () => SubmenuFilterIsReviews()),
                new Option("Size", () => SubmenuFilterIsSize()),
                new Option("Return To Main Menu", () => Mainmenu())
            });
            ChangeUserMenu.MenuLoopInConsole();

        }
        public static void SubmenuFilterIsPrice()
        {
            //Console.Clear();
            //Console.WriteLine("What will you filter\n");

            ////DataLoader.FilterApps(myEnums.Filter.Price, myEnums.Operator.greater_or_equals, 2)

            //ConsoleMenu ChangeUserMenu = new ConsoleMenu(new Option[]{
            //    new Option("Price", () => DataLoader.FilterApps(myEnums.Filter.Price, myEnums.Operator.greater_or_equals, 2)),
            //    new Option("Reviews", () => DataLoader.FilterApps(myEnums.Filter.Reviews, myEnums.Operator.greater_or_equals, 2)),
            //    new Option("Size", () => DataLoader.FilterApps(myEnums.Filter.Size, myEnums.Operator.greater_or_equals, 2)),
            //    new Option("Return To Main Menu", () => Mainmenu())
            //});
            //ChangeUserMenu.MenuLoopInConsole();

        }
        public static void SubmenuFilterIsReviews()
        {
            //Console.Clear();
            //Console.WriteLine("What will you filter\n");

            ////DataLoader.FilterApps(myEnums.Filter.Reviews, myEnums.Operator.greater_or_equals, 2))

            //ConsoleMenu ChangeUserMenu = new ConsoleMenu(new Option[]{
            //    new Option("Price", () => DataLoader.FilterApps(myEnums.Filter.Price, myEnums.Operator.greater_or_equals, 2)),
            //    new Option("Reviews", () => DataLoader.FilterApps(myEnums.Filter.Reviews, myEnums.Operator.greater_or_equals, 2)),
            //    new Option("Size", () => DataLoader.FilterApps(myEnums.Filter.Size, myEnums.Operator.greater_or_equals, 2)),
            //    new Option("Return To Main Menu", () => Mainmenu())
            //});
            //ChangeUserMenu.MenuLoopInConsole();

        }
        public static void SubmenuFilterIsSize()
        {
            //Console.Clear();
            //Console.WriteLine("What will you filter\n");

            ////DataLoader.FilterApps(myEnums.Filter.Size, myEnums.Operator.greater_or_equals, 2)

            //ConsoleMenu ChangeUserMenu = new ConsoleMenu(new Option[]{
            //    new Option("Price", () => DataLoader.FilterApps(myEnums.Filter.Price, myEnums.Operator.greater_or_equals, 2)),
            //    new Option("Reviews", () => DataLoader.FilterApps(myEnums.Filter.Reviews, myEnums.Operator.greater_or_equals, 2)),
            //    new Option("Size", () => DataLoader.FilterApps(myEnums.Filter.Size, myEnums.Operator.greater_or_equals, 2)),
            //    new Option("Return To Main Menu", () => Mainmenu())
            //});
            //ChangeUserMenu.MenuLoopInConsole();

        }
        #endregion

        public static void DisplayAppsFromListToConsole(List<AppData> apps)
        {
            Console.Clear();

            foreach (AppData app in apps)
            {
                Console.WriteLine(app.AppDataString());
            }

            Console.WriteLine("\n\nPress any key to go back...");
            Console.ReadLine();
        }

        public static void DisplayErrorLines(List<int> errors)
        {
            Console.Clear();
            Console.WriteLine("Errors occured during reading AppData from Files:\n");

            int filecount = 1;
            int counter = 0;
            int loopcount = 0;
            Console.Write("File 1 Error in Line: ");

            foreach (int errorLine in errors)
            {
                loopcount++;
                if (errorLine == -1)
                {
                    if (counter==0)
                    {
                        Console.Write("No Errors");
                    }
                    filecount++;
                    counter = 0;

                    if (loopcount!=errors.Count)
                    {
                        Console.Write("\n");
                        Console.Write("File " + filecount + " Error in Line: ");
                    }
                }
                else
                {
                    counter++;
                    Console.Write(errorLine + " ");
                }
            }

            Console.WriteLine("\n\nPress any key to go back...");
            Console.ReadLine();
        }
    }
}
