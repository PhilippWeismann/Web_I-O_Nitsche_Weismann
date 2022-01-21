using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Web_I_O_Nitsche_Weismann
{
    static class DataLoader
    {
        #region Members

        static List<AppData> _allApps = new List<AppData>();

        static List<AppData> _filteredApps = new List<AppData>();

        static List<int> _errorLines = new List<int>();
        

        #endregion

        #region Properties

        public static List<AppData> AllApps
        {
            get
            {
                return _allApps;
            }
            set
            {
                _allApps = value;
            }
        }

        public static List<AppData> FilteredApps
        {
            get
            {
                return _filteredApps;
            }
            set
            {
                _filteredApps = value;
            }
        }

        public static List<int> ErrorLines
        {
            get
            {
                return _errorLines;
            }
            set
            {
                _errorLines = value;
            }
        }

        #endregion

        #region Methods
        public static void ReadAppsFromURL(char seperator, string filePath)
        {
            

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
                        AllApps.Add(ConvertLineToApp(line, seperator));
                    }
                    catch (Exception)
                    {
                        // throw auskommentiert , das Programm weiterlaufen soll um zu berechnen (Fail silent)
                        // throw würde Aktiv werden wenn eine Exception bei den Set-Properties auftritt (Wertebereich)
                        // oder beim var.Parse in ConvertLineToApp

                        /*throw*/
                        new Exception("Line (Number: " + counter.ToString() + ") could not be convertet to valid App.");

                        ErrorLines.Add(counter);
                    }
                }

                counter++;
            }
            myStreamReader.Close();

            ErrorLines.Add(-1);
        }

        public static void FilterApps(myEnums.Filter filter, myEnums.Operator compareOperator ,double filterValue)
        {

            switch (filter)
            {
                case myEnums.Filter.Price:
                    switch (compareOperator)
                    {
                        case myEnums.Operator.greater_or_equals:
                            foreach (AppData app in AllApps)
                            {
                                if (app.Price >= filterValue)
                                {
                                    FilteredApps.Add(app);
                                }
                            }
                            break;

                        case myEnums.Operator.less_or_equals:
                            foreach (AppData app in AllApps)
                            {
                                if (app.Price <= filterValue)
                                {
                                    FilteredApps.Add(app);
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                case myEnums.Filter.Reviews:
                    switch (compareOperator)
                    {
                        case myEnums.Operator.greater_or_equals:
                            foreach (AppData app in AllApps)
                            {
                                if (app.Reviews >= filterValue)
                                {
                                    FilteredApps.Add(app);
                                }
                            }
                            break;

                        case myEnums.Operator.less_or_equals:
                            foreach (AppData app in AllApps)
                            {
                                if (app.Reviews <= filterValue)
                                {
                                    FilteredApps.Add(app);
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                case myEnums.Filter.Size:
                    double size = 0;
                    switch (compareOperator)
                    {
                        case myEnums.Operator.greater_or_equals:
                            foreach (AppData app in AllApps)
                            {

                                if (app.Size.EndsWith("M"))
                                {
                                    string sizeString = "35.8M";//app.Size.ToString();
                                    string test = sizeString.Remove((sizeString.Length -1), 1);
                                    sizeString = "0";
                                    try
                                    {
                                        size = double.Parse(sizeString);
                                    }
                                    catch (Exception)
                                    {
                                        new Exception("oups");
                                    }

                                }

                                //string number = app.Size.
                                if (size >= filterValue || app.Size.Equals("Varies with device"))
                                {
                                    FilteredApps.Add(app);
                                }
                            }
                            break;

                        case myEnums.Operator.less_or_equals:
                            foreach (AppData app in AllApps)
                            {
                                if (app.Size[app.Size.Length - 1].Equals('M'))
                                {
                                    app.Size.TrimEnd('M');
                                    size = double.Parse(app.Size);
                                }

                                //string number = app.Size.
                                if (size <= filterValue || app.Size.Equals("Varies with device"))
                                {
                                    FilteredApps.Add(app);
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }

        }


        private static AppData ConvertLineToApp(string line, char seperator)
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
