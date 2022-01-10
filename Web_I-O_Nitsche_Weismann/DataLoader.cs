using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_I_O_Nitsche_Weismann
{
    class DataLoader
    {
        #region readfromtxt
        public static List<AppData> ReadAppsFromFile(char seperator, string filePath)
        {
            List<AppData> myApps = new List<AppData>();

            StreamReader myStreamReader = new StreamReader(filePath);
            string line;
            int counter = 1;

            while (myStreamReader.Peek() != -1)
            {
                line = myStreamReader.ReadLine();

                if (counter > 1)
                {
                    try
                    {
                        myApps.Add(ConvertLineToApp(line, seperator));
                    }
                    catch (Exception)
                    {
                        // throw auskommentiert , das Programm weiterlaufen soll um zu berechnen (Fail silent)
                        // throw würde Aktiv werden wenn eine Exception bei den Set-Properties auftritt (Wertebereich)
                        // oder beim var.Parse in ConvertLineToProduct

                        /*throw*/
                        new Exception("Line (Number: " + counter.ToString() + ") could not be convertet to valid App.");
                    }
                }

                counter++;
            }
            myStreamReader.Close();

            return myApps;
        }
        public static AppData ConvertLineToApp(string line, char seperator)
        {
            AppData app = new AppData();

            string[] parts = line.Split(seperator);

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Replace('.', ',');
            }

            app.AppName = parts[0];
            app.Category = (myEnums.Category)Enum.Parse(typeof(myEnums.Category), parts[1]);
            app.Rating = int.Parse(parts[2]);
            app.Reviews = int.Parse(parts[3]);
            app.Size = parts[4];
            app.Installs = int.Parse(parts[5].TrimEnd('+'));
            app.PriceType = (myEnums.PriceType)Enum.Parse(typeof(myEnums.PriceType), parts[6]);
            app.ContentRating = (myEnums.ContentRating)Enum.Parse(typeof(myEnums.ContentRating), parts[7]);
            app.Genres = (myEnums.Genres)Enum.Parse(typeof(myEnums.Genres), parts[8]);
            app.LastUpdated = DateTime.Parse(parts[9]);
            app.CurrentVersion = parts[10];
            app.AndroidVersion = parts[11];

            return app;
        }
        #endregion
    }
}
