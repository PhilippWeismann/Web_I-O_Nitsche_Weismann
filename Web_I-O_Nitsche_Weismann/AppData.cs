using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_I_O_Nitsche_Weismann
{
    class AppData
    {
        #region Members

        string _appName;
        int _rating;
        int _reviews;
        double _size;
        long _installs;
        double _price;
        DateTime _lastUpdated;
        string _currentVersion;
        string _androidVersion;

        myEnums.Category _category;
        myEnums.PriceType _priceType;
        myEnums.ContentRating _contentRating;
        myEnums.Genres _genres;

        #endregion

        #region Properties (public get / private set) + Data Validation

        public string AppName
        {
            get
            {
                return _appName;
            }
            set
            {
                if (value.Length > 2)
                {
                    _appName = value;
                }
                else
                {
                    throw new Exception("Invalid App Name");
                }
            }
        }

        public int Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                if (value < 0)
                {
                    _rating = value;
                }
                else
                {
                    throw new Exception("Invalid Rating Value");
                }
            }
        }

        public int Reviews
        {
            get
            {
                return _reviews;
            }
            set
            {
                if (value < 0)
                {
                    _reviews = value;
                }
                else
                {
                    throw new Exception("Invalid Reviews Value");
                }
            }
        }

        public double Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (value < 0)
                {
                    _size = value;
                }
                else
                {
                    throw new Exception("Invalid Size Value");
                }
            }
        }

        public long Installs
        {
            get
            {
                return _installs;
            }
            set
            {
                if (value < 0)
                {
                    _installs = value;
                }
                else
                {
                    throw new Exception("Invalid Installs Value");
                }
            }
        }

        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value < 0)
                {
                    _price = value;
                }
                else
                {
                    throw new Exception("Invalid Price Value");
                }
            }
        }

        public DateTime LastUpdated
        {
            get
            {
                return _lastUpdated;
            }
            set
            {
                if (value > DateTime.Now)
                {
                    _lastUpdated = value;
                }
                else
                {
                    throw new Exception("Invalid Last Updated Date");
                }
            }
        }

        public string CurrentVersion
        {
            get
            {
                return _currentVersion;
            }
            set
            {
                if (value.Length > 0)
                {
                    _currentVersion = value;
                }
                else
                {
                    throw new Exception("Invalid App-Version Name");
                }
            }
        }

        public string AndroidVersion
        {
            get
            {
                return _androidVersion;
            }
            set
            {
                if (value.Length > 0)
                {
                    _androidVersion = value;
                }
                else
                {
                    throw new Exception("Invalid Android-Version Name");
                }
            }
        }

        public myEnums.Category Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }

        public myEnums.PriceType PriceType
        {
            get
            {
                return _priceType;
            }
            set
            {
                _priceType = value;
            }
        }

        public myEnums.ContentRating ContentRating
        {
            get
            {
                return _contentRating;
            }
            set
            {
                _contentRating = value;
            }
        }

        public myEnums.Genres Genres
        {
            get
            {
                return _genres;
            }
            set
            {
                _genres = value;
            }
        }

        #endregion

        #region Konstruktors
        public AppData()
        {

        }

        public AppData(string appName, int rating, int reviews, double size, long installs, double price, DateTime lastUpdated, string currentVersion, string androidVersion, myEnums.Category category, myEnums.PriceType priceType, myEnums.ContentRating contentRating, myEnums.Genres genres)
        {
            AppName = appName;
            Rating = rating;
            Reviews = reviews;
            Size = size;
            Installs = installs;
            Price = price;
            LastUpdated = lastUpdated;
            CurrentVersion = currentVersion;
            AndroidVersion = androidVersion;
            Category = category;
            PriceType = priceType;
            ContentRating = contentRating;
            Genres = genres;
        }
        #endregion


        #region Methods

        public string AppDataString()
        {

            string s = string.Format($"" +
                $"{ AppName, 30} " +
                $"{ Category.ToString(), 30} " +
                $"{ Rating.ToString(), 3} " +
                $"{ Reviews.ToString(), 10} " +
                $"{ Size.ToString() + "M",10} " +
                $"{ Installs.ToString() + "+",15} " +
                $"{ PriceType.ToString(), 8} " +
                $"{ ContentRating.ToString(), 20} " +
                $"{ Genres.ToString(), 20} " +
                $"{ LastUpdated.ToString(), 12} " +
                $"{ CurrentVersion.ToString(), 10} " +
                $"{ AndroidVersion.ToString(), 10}");

            return s;
        }

        ////Returns a List of all valid Weather Data from given File (filepath)
        //public static List<AppData> ReadWeatherDataFromFile(char seperator, string filePath)
        //{
        //    List<AppData> myWeatherDataList = new List<AppData>();

        //    StreamReader myStreamReader = new StreamReader(filePath);
        //    string line;
        //    int counter = 1;

        //    while (myStreamReader.Peek() != -1)
        //    {
        //        line = myStreamReader.ReadLine();

        //        if (counter > 1)
        //        {
        //            try
        //            {
        //                myWeatherDataList.Add(ConvertLineToWeatherData(line, seperator));
        //            }
        //            catch (Exception)
        //            {
        //                // throw auskommentiert , das Programm weiterlaufen soll um zu berechnen (Fail silent)
        //                // throw würde Aktiv werden wenn eine Exception bei den Set-Properties auftritt (Wertebereich)
        //                // oder beim var.Parse in ConvertLineToWeatherData

        //                /*throw*/
        //                new Exception("Line (Number: " + counter.ToString() + ") could not be convertet to valid Weather Data.");
        //            }
        //        }

        //        counter++;
        //    }
        //    myStreamReader.Close();

        //    return myWeatherDataList;
        //}

        ////converts a line from .csv File to an Object Weatherdata
        //public static AppData ConvertLineToWeatherData(string line, char seperator)
        //{
        //    AppData weatherData = new AppData();

        //    string[] parts = line.Split(seperator);

        //    for (int i = 0; i < parts.Length; i++)
        //    {
        //        parts[i] = parts[i].Replace('.', ',');
        //    }


        //    weatherData.Date = DateTime.Parse(parts[0]);
        //    weatherData.AverageTemperature = double.Parse(parts[1]);
        //    weatherData.MinimumTemperature = double.Parse(parts[2]);
        //    weatherData.MaximumTemperature = double.Parse(parts[3]);
        //    weatherData.MaximumPrecipitation = double.Parse(parts[4]);
        //    weatherData.Snow = double.Parse(parts[5]);
        //    weatherData.WindDirection = double.Parse(parts[6]);
        //    weatherData.WindSpeed = double.Parse(parts[7]);
        //    weatherData.WindPeakGust = double.Parse(parts[8]);
        //    weatherData.Pressure = double.Parse(parts[9]);
        //    weatherData.SunTime = int.Parse(parts[10]);

        //    return weatherData;
        //}

        //// Outputs a string of Year and Month in form of (yyyy/MM)
        //public static string YearMonthString(AppData weatherData)
        //{
        //    return weatherData.Date.Year.ToString() + "-" + weatherData.Date.Month.ToString();
        //}

        ////Returns an Array usedMonths which contains the months where vaid Data is given
        //public static string[] UsedMonthsInData(List<AppData> List)
        //{
        //    List<string> usedMonth = new List<string>() { YearMonthString(List[0]) };

        //    //Generates List usedMonths which contains the months where vaid Data is given
        //    foreach (AppData weatherData in List)
        //    {
        //        string yearMonth = YearMonthString(weatherData);
        //        bool missing = false;

        //        foreach (string item in usedMonth)
        //        {
        //            if (yearMonth != item)
        //            {
        //                missing = true;
        //            }
        //            else
        //            {
        //                missing = false;
        //                break;
        //            }
        //        }
        //        if (missing == true)
        //        {
        //            usedMonth.Add(yearMonth);
        //        }
        //    }

        //    return usedMonth.ToArray();
        //}

        //// outputs an array with monthly average temperatures and suntimes; additonally infoArray[Name of Month , number of valid data sets in this month]
        //public static double[,] CalculateMonthlyAverageTemperatureAndSunMinutes(List<AppData> List, out string[,] infoArray)
        //{
        //    string[] usedMonth = UsedMonthsInData(List);

        //    // monthlymeans[AverageTemperature, AverageSunMinutes]
        //    double[,] monthlyMeans = new double[usedMonth.Count(), 2];
        //    int[] counters = new int[usedMonth.Count()];

        //    //Calculates the Sum of Temperatures and SunTime and the Number of ValidData Sets in this Month
        //    for (int i = 0; i < List.Count(); i++)
        //    {
        //        for (int j = 0; j < usedMonth.Count(); j++)
        //        {
        //            if (YearMonthString(List[i]) == usedMonth[j])
        //            {
        //                monthlyMeans[j, 0] += List[i].AverageTemperature;
        //                monthlyMeans[j, 1] += List[i].SunTime;
        //                counters[j]++;
        //                break;
        //            }
        //        }
        //    }

        //    //Devides the Sums through the number of the given valid data sets
        //    for (int i = 0; i < monthlyMeans.GetLength(0); i++)
        //    {
        //        monthlyMeans[i, 0] = monthlyMeans[i, 0] / counters[i];
        //        monthlyMeans[i, 1] = monthlyMeans[i, 1] / counters[i];
        //    }

        //    //Rounds the double-Values
        //    for (int i = 0; i < monthlyMeans.GetLength(0); i++)
        //    {
        //        monthlyMeans[i, 0] = Math.Round(monthlyMeans[i, 0], 3);
        //        monthlyMeans[i, 1] = Math.Round(monthlyMeans[i, 1], 3);
        //    }

        //    //Fills InformationArray contains [string of Month, string of valid Datasets in this Month]
        //    infoArray = new string[usedMonth.Length, 2];
        //    for (int i = 0; i < usedMonth.Length; i++)
        //    {
        //        infoArray[i, 0] = usedMonth[i];
        //        infoArray[i, 1] = counters[i].ToString();
        //    }

        //    return monthlyMeans;
        //}

        #endregion
    }
    }
