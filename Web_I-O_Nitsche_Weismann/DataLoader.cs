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
        public static List<AppData> ReadAppsFromFile(char seperator, string filePath, List<int> errorBuffer)
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

                        errorBuffer.Add(counter);
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


            //Replacement not needed because every number is written with komma
            //for (int i = 0; i < parts.Length; i++)
            //{
            //    parts[i] = parts[i].Replace('.', ',');
            //}


            // App Name
            app.AppName = parts[0];

            //Category
            app.Category = CategoryAsEnum(parts[1], out bool categoryIsEnum);
            if (!categoryIsEnum) throw new Exception("Category is no Enum");

            //Rating
            app.Rating = int.Parse(parts[2]);

            //Reviews
            app.Reviews = int.Parse(parts[3]);

            //Size
            app.Size = parts[4];

            //Installs
            app.Installs = parts[5];

            //Price Type
            app.PriceType = (myEnums.PriceType)Enum.Parse(typeof(myEnums.PriceType), parts[6]);

            //Price
            app.Price = double.Parse(parts[7]);

            //Content Rating
            app.ContentRating = ContentRatingAsEnum(parts[8], out bool contentRatingIsEnum);
            if (!contentRatingIsEnum) throw new Exception("Content rating is no Enum");

            //Genres
            app.Genres = GenresAsEnum(parts[9], out bool genresIsEnum);
            if (!genresIsEnum) throw new Exception("Genre is no Enum");

            // Last Updated
            app.LastUpdated = DateTime.Parse(parts[10]);

            //Current Version
            app.CurrentVersion = parts[11];

            //Android Version
            app.AndroidVersion = parts[12];

            return app;
        }
        #endregion

        #region Methods
        private static myEnums.Category CategoryAsEnum(string categoryAsString, out bool isEnum)
        {
            switch (categoryAsString)
            {
                case "WEATHER":
                    isEnum = true;
                    return myEnums.Category.Weather;

                case "HEALTH_AND_FITNESS":
                    isEnum = true;
                    return myEnums.Category.Health_and_Fitness;

                case "PHOTOGRAPHY":
                    isEnum = true;
                    return myEnums.Category.Photography;

                default:
                    isEnum = false;
                    return myEnums.Category.Health_and_Fitness;
            }
        }
        private static myEnums.ContentRating ContentRatingAsEnum(string contentRatingAsString, out bool isEnum)
        {
            switch (contentRatingAsString)
            {
                case "Everyone":
                    isEnum = true;
                    return myEnums.ContentRating.Everyone;

                case "Everyone 10+":
                    isEnum = true;
                    return myEnums.ContentRating.Everyone_10;

                case "Mature 17+":
                    isEnum = true;
                    return myEnums.ContentRating.Mature_17;

                case "Teen":
                    isEnum = true;
                    return myEnums.ContentRating.Teen;

                default:
                    isEnum = false;
                    return myEnums.ContentRating.Everyone;
            }
        }
        private static myEnums.Genres GenresAsEnum(string genresAsString, out bool isEnum)
        {
            switch (genresAsString)
            {
                case "Health & Fitness":
                    isEnum = true;
                    return myEnums.Genres.Health_Fitness;

                case "Weather":
                    isEnum = true;
                    return myEnums.Genres.Weather;

                case "Photography":
                    isEnum = true;
                    return myEnums.Genres.Photography;

                default:
                    isEnum = false;
                    return myEnums.Genres.Weather;
            }
        }
        #endregion
    }
}
