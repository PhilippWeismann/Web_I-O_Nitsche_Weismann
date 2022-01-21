using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Web_I_O_Nitsche_Weismann
{
    class DataLoader
    {
        #region readfromtxt
        public static List<AppData> ReadAppsFromFile(char seperator, string filePath, out List<int> errors)
        {
            List<AppData> myApps = new List<AppData>();


            //string bufferFilePath = @"\..\..\buffer.txt";

            //if (File.Exists(bufferFilePath))
            //{ // Create a file to write to
            //    File.Delete(bufferFilePath);
            //}
            //File.Create(bufferFilePath);


            WebClient myWebclient = new WebClient();

            Stream myStream = myWebclient.OpenRead(filePath);

            StreamReader myStreamReader = new StreamReader(myStream);


            string line;
            int counter = 1;
            List<int> errorList = new List<int>();


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

                        errorList.Add(counter);
                    }
                }

                counter++;
            }
            myStreamReader.Close();

            errors = errorList;
            return myApps;
        }
        public static AppData ConvertLineToApp(string line, char seperator)
        {
            AppData app = new AppData();

            string[] parts = line.Split(seperator);

            //for (int i = 0; i < parts.Length; i++)
            //{
            //    parts[i] = parts[i].Replace('.', ',');
            //}

            app.AppName = parts[0];
            app.Category = (myEnums.Category)Enum.Parse(typeof(myEnums.Category), parts[1]);
            app.Rating = int.Parse(parts[2]);
            app.Reviews = int.Parse(parts[3]);
            app.Size = parts[4];
            app.Installs = parts[5];
            app.PriceType = (myEnums.PriceType)Enum.Parse(typeof(myEnums.PriceType), parts[6]);
            app.Price = double.Parse(parts[7]);
            //app.ContentRating = (myEnums.ContentRating)Enum.Parse(typeof(myEnums.ContentRating), parts[8]);
            //app.Genres = (myEnums.Genres)Enum.Parse(typeof(myEnums.Genres), parts[9]);
            app.LastUpdated = DateTime.Parse(parts[10]);
            app.CurrentVersion = parts[11];
            app.AndroidVersion = parts[12];

            //for development:
            app.ContentRating = myEnums.ContentRating.Everyone;
            app.Genres = myEnums.Genres.Health_Fitness;

            return app;
        }
        #endregion
    }
}
