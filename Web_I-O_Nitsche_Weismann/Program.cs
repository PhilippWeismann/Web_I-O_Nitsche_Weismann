using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop_Nitsche_Weismann; //for the menu

namespace Web_I_O_Nitsche_Weismann
{
    class Program
    {
        static int countOfFiltering = 0;

        static void Main(string[] args)
        {
            Settings();
            
            string filePathHealthFitness = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/HealthFitnessApps.CSV";
            string filePathPhotpgraphy = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/PhotographyApps.CSV";
            string filePathWeather = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/WeatherApps.CSV";

            DataLoader.ReadAppsFromURL(';', filePathHealthFitness);
            DataLoader.ReadAppsFromURL(';', filePathPhotpgraphy);
            DataLoader.ReadAppsFromURL(';', filePathWeather);

            Mainmenu();
        }

        #region Menu - Methods
        public static void Mainmenu()
        {
            // Main Menu
            bool exit = false;
            ConsoleMenu Mainmenu = new ConsoleMenu(new Option[]{
                new Option("Display all Apps from Database", () => DisplayAppsFromListToConsole(DataLoader.AllApps)),
                new Option("Filter Apps (multiple times possible, old filters are retained)", () => SubmenuFilterApps()),
                new Option("Reset filters", () => countOfFiltering=0),
                new Option("Show lines where conversion from Database didn't work", () => DisplayErrorLines(DataLoader.ErrorLines)),
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
            Console.WriteLine("By what do you want to filter? (filtercriteria - less/greater than - filtervalue)\n");

            ConsoleMenu ChangeUserMenu = new ConsoleMenu(new Option[]{
                new Option("Price", () => SubmenuOperator(myEnums.Filter.Price)),
                new Option("Reviews", () => SubmenuOperator(myEnums.Filter.Reviews)),
                new Option("Size", () => SubmenuOperator(myEnums.Filter.Size)),
                new Option("Return To Main Menu", () => Mainmenu())
            });
            ChangeUserMenu.MenuLoopInConsole();

        }
        public static void SubmenuOperator(myEnums.Filter filter)
        {
            Console.Clear();
            Console.WriteLine("Should the filterd Criteria be less or greater than the given filtervalue?\n");

            //DataLoader.FilterApps(myEnums.Filter.Price, myEnums.Operator.greater_or_equals, 2)

            ConsoleMenu ChangeUserMenu = new ConsoleMenu(new Option[]{
                new Option(">= (greater or equals)", () => SubmenuFilterValue(filter,myEnums.Operator.greater_or_equals)),
                new Option("<= (less or equals)", () => SubmenuFilterValue(filter,myEnums.Operator.less_or_equals)),
                new Option("Return To Main Menu", () => Mainmenu())
            });
            ChangeUserMenu.MenuLoopInConsole();
        }
        public static void SubmenuFilterValue(myEnums.Filter filter, myEnums.Operator operatorOfFilter)
        {
            Console.Clear();
            Console.WriteLine("Please enter your filtervalue?\n\nPlease input the Value:");
            String valueAsString = Console.ReadLine();

            if (int.TryParse(valueAsString, out int value))
            {
                if (countOfFiltering<1)     //true by the first filter
                {
                    DataLoader.FilterApps(DataLoader.AllApps, filter, operatorOfFilter, value);
                }
                else
                {
                    DataLoader.FilterApps(DataLoader.FilteredApps, filter, operatorOfFilter, value);
                }

                DataLoader.AppsToCsv();     //to generate csv data

                //Console.WriteLine("The Apps are filtered by: " + filter + operatorOfFilter + value);
                DisplayAppsFromListToConsole(DataLoader.FilteredApps);
                              
                countOfFiltering += 1;
            }
            else
            {
                Console.WriteLine("This value isn't possible!\n\nPress Key to go back");
                Console.ReadLine();
            }                    
        }
        #endregion

        #region Methods
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
            Console.WriteLine("Errors occured during reading AppData from Database:\n");

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
        public static void Settings()
        {
            //Console Settings
            Console.Title = "Web - IO | © Simon Nitsche & Philipp Weismann";
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetWindowSize(Console.LargestWindowWidth-18, Console.LargestWindowHeight-7);

            Console.SetWindowPosition(0, 0);
        }// Settings for Console-Appearance
        #endregion
    }
}
