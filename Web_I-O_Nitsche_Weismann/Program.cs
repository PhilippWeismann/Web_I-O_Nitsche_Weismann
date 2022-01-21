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
            List<int> readingErrors = new List<int>();

            string filePathHealthFitness = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/HealthFitnessApps.CSV";
            string filePathPhotpgraphy = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/PhotographyApps.CSV";
            string filePathWeather = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/WeatherApps.CSV";

            List<AppData> apps = LoadDataFromLinks(filePathHealthFitness, filePathPhotpgraphy, filePathWeather, out readingErrors);

            DisplayAllValidAppsToConsole(apps);

            DisplayErrorLines(readingErrors);


            Console.ReadKey();
        }

        static List<AppData> LoadDataFromLinks(string filePath1, string filePath2, string filePath3, out List<int> errorLines)
        {
            List<AppData> apps = new List<AppData>();

            List<int> errorList = new List<int>();
            List<int> errorBuffer = new List<int>();




            apps.AddRange(DataLoader.ReadAppsFromFile(';', filePath1, out errorBuffer));
            errorList.AddRange(errorBuffer);


            apps.AddRange(DataLoader.ReadAppsFromFile(';', filePath2, out errorBuffer));
            errorList.AddRange(errorBuffer);

            apps.AddRange(DataLoader.ReadAppsFromFile(';', filePath3, out errorBuffer));
            errorList.AddRange(errorBuffer);

            errorLines = errorList;

            return apps;
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

            int line = 0;
            int filecount = 1;
            Console.Write("File 1 Error Lines: ");

            foreach (int errorLine in errors)
            {
                if (errorLine < line)
                {
                    filecount++;
                    Console.Write("\n");
                    Console.Write("File " + filecount + " Error Lines: ");
                }

                Console.Write(errorLine + " ");

                line = errorLine;
            }
        }
    }
}
