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
        static int countOfFiltering = 0;

        static void Main(string[] args)
        {
            
            string filePathHealthFitness = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/HealthFitnessApps.CSV";
            string filePathPhotpgraphy = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/PhotographyApps.CSV";
            string filePathWeather = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/WeatherApps.CSV";

            DataLoader.ReadAppsFromURL(';', filePathHealthFitness);
            DataLoader.ReadAppsFromURL(';', filePathPhotpgraphy);
            DataLoader.ReadAppsFromURL(';', filePathWeather);

            //DisplayAppsFromListToConsole(DataLoader.AllApps);

            //DataLoader.FilterApps(DataLoader.AllApps,myEnums.Filter.Size, myEnums.Operator.less_or_equals, 30);
            //DisplayAppsFromListToConsole(DataLoader.FilteredApps);
            //int i = DataLoader.FilteredApps.Count();


            //DataLoader.FilterApps(DataLoader.FilteredApps, myEnums.Filter.Size, myEnums.Operator.less_or_equals, 10);
            //DisplayAppsFromListToConsole(DataLoader.FilteredApps);
            //int j = DataLoader.FilteredApps.Count();


            //DataLoader.FilterApps(DataLoader.FilteredApps, myEnums.Filter.Price, myEnums.Operator.greater_or_equals, 5);
            //DisplayAppsFromListToConsole(DataLoader.FilteredApps);
            //int k = DataLoader.FilteredApps.Count();

            //Console.WriteLine("Durchgang1: " + i + "   Durchgang2: " + j + "   Durchgang3: " + k);

            //Console.ReadLine();

            //DisplayAppsFromListToConsole(DataLoader.FilteredApps);

            //DisplayAppsFromListToConsole(DataLoader.AllApps);

            Mainmenu();

        }

        #region Menu
        public static void Mainmenu()
        {
            // Main Menu
            bool exit = false;
            ConsoleMenu Mainmenu = new ConsoleMenu(new Option[]{
                new Option("Show all Apps", () => DisplayAppsFromListToConsole(DataLoader.AllApps)),
                new Option("Filter Apps (more often possible, old filters are retained)", () => SubmenuFilterApps()),
                new Option("Rest filters", () => countOfFiltering=0),
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
            Console.WriteLine("Will you filter lesser or greater Apps\n");

            //DataLoader.FilterApps(myEnums.Filter.Price, myEnums.Operator.greater_or_equals, 2)

            ConsoleMenu ChangeUserMenu = new ConsoleMenu(new Option[]{
                new Option(">=", () => SubmenuFilterValue(filter,myEnums.Operator.greater_or_equals)),
                new Option("<=", () => SubmenuFilterValue(filter,myEnums.Operator.less_or_equals)),
                new Option("Return To Main Menu", () => Mainmenu())
            });
            ChangeUserMenu.MenuLoopInConsole();
        }
        public static void SubmenuFilterValue(myEnums.Filter filter, myEnums.Operator operatorOfFilter)
        {
            Console.Clear();
            Console.WriteLine("What is your filtervalue?\n\nPlease input the Value:");
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

                //Console.WriteLine("The Apps a filtered by: " + filter + operatorOfFilter + value);
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
