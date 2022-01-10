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
            string filePathHealthFitness = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/HealthFitnessApps.CSV";
            string filePathPhotpgraphy = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/PhotographyApps.CSV";
            string filePathWeather = @"https://fhwels.s3.eu-central-1.amazonaws.com/PRO1UE_WS21/WeatherApps.CSV";

            LoadDataFromLinks(filePathHealthFitness, filePathPhotpgraphy, filePathWeather);
        }

        static List<AppData> LoadDataFromLinks(string filePath1, string filePath2, string filePath3)
        {
            List<AppData> apps = new List<AppData>();

            apps.AddRange(DataLoader.ReadAppsFromFile(';', filePath1));
            apps.AddRange(DataLoader.ReadAppsFromFile(';', filePath2));
            apps.AddRange(DataLoader.ReadAppsFromFile(';', filePath3));

            return apps;
        }
    }
}
