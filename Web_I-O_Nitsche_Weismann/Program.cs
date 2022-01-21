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

            //DisplayAppsFromListToConsole(DataLoader.AllApps);

            DataLoader.FilterApps(DataLoader.AllApps,myEnums.Filter.Size, myEnums.Operator.less_or_equals, 30);
            DisplayAppsFromListToConsole(DataLoader.FilteredApps);
            int i = DataLoader.FilteredApps.Count();


            DataLoader.FilterApps(DataLoader.FilteredApps, myEnums.Filter.Size, myEnums.Operator.less_or_equals, 10);
            DisplayAppsFromListToConsole(DataLoader.FilteredApps);
            int j = DataLoader.FilteredApps.Count();

            Mainmenu();
            DataLoader.FilterApps(DataLoader.FilteredApps, myEnums.Filter.Price, myEnums.Operator.greater_or_equals, 5);
            DisplayAppsFromListToConsole(DataLoader.FilteredApps);
            int k = DataLoader.FilteredApps.Count();

            //DisplayAppsFromListToConsole(DataLoader.FilteredApps);

            //DisplayAppsFromListToConsole(DataLoader.AllApps);

        }

        #region Mainmenu
        public static void Mainmenu()
        {
            // Main Menu
            bool exit = false;
            ConsoleMenu Mainmenu = new ConsoleMenu(new Option[]{
                new Option("Show all Apps", () => DisplayAppsFromListToConsole(DataLoader.AllApps)),
                new Option("Filter Apps", () => DisplayAppsFromListToConsole(DataLoader.FilteredApps)),
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
