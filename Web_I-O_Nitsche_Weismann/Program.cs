using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_I_O_Nitsche_Weismann
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> errorLines = new List<int>();

            string filePathHealthFitness = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/HealthFitnessApps.CSV";
            string filePathPhotpgraphy = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/PhotographyApps.CSV";
            string filePathWeather = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/WeatherApps.CSV";

            List<AppData> apps = LoadDataFromLinks(filePathHealthFitness, filePathPhotpgraphy, filePathWeather, errorLines);

            DisplayAllValidAppsToConsole(apps);

            DisplayErrorLines(errorLines);


            Console.ReadKey();
        }

        static List<AppData> LoadDataFromLinks(string filePath1, string filePath2, string filePath3, List<int> errorLines)
        {
            List<AppData> apps = new List<AppData>();

            AddDataFromURL(apps, filePath1, errorLines);
            AddDataFromURL(apps, filePath2, errorLines);
            AddDataFromURL(apps, filePath3, errorLines);

            return apps;
        }

        public static void AddDataFromURL(List<AppData> apps, string filePath, List<int> errorLines)
        {
            List<int> errorBuffer = new List<int>();
            apps.AddRange(DataLoader.ReadAppsFromFile(';', filePath, errorBuffer));
            errorLines.AddRange(errorBuffer);
            errorLines.Add(-1);
        }

        public static void DisplayAllValidAppsToConsole(List<AppData> apps)
        {
            foreach (AppData app in apps)
            {
                Console.WriteLine(app.AppDataString());
            }
        }

        public static void DisplayErrorLines(List<int> errors)
        {
            Console.WriteLine("Error in Lines:\n");

            int filecount = 1;
            int counter = 0;
            int loopcount = 0;
            Console.Write("File 1 Error Lines: ");

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
                        Console.Write("File " + filecount + " Error Lines: ");
                    }
                }
                else
                {
                    counter++;
                    Console.Write(errorLine + " ");
                }
            }
            Console.WriteLine();
        }
    }
}
