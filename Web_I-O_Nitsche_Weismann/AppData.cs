﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_I_O_Nitsche_Weismann
{
    class AppData
    {
        #region Members (date,tavg,tmin,tmax,prcp,snow,wdir,wspd,wpgt,pres,tsun)

        string _appName;
        myEnums _category;
        int _rating;
        int _reviews;
        double _size;
        decimal _installs;
        myEnums _priceType;
        double _price;
        myEnums _contentRating;
        myEnums _genres;
        DateTime _lastUpdated;
        string _currentVersion;
        string _androidVersion;

        #endregion

        #region Properties (public get / private set) + Data Validation

        public string AppName
        {
            get
            {
                return _appName;
            }
            private set
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
            private set
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
            private set
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
            private set
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

        public decimal Installs
        {
            get
            {
                return _installs;
            }
            private set
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
            private set
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
            private set
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
            private set
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
            private set
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

        #endregion

        #region Konstruktors
        public AppData()
        {

        }
        #endregion

        //public AppData(string date, double tavg, double tmin, double tmax, double prcp, double snow, double wdir, double wspd, double wpgt, double pres, int tsun)
        //{
        //    Date = date;
        //    AverageTemperature = tavg;
        //    MinimumTemperature = tmin;
        //    MaximumTemperature = tmax;
        //    MaximumPrecipitation = prcp;
        //    Snow = snow;
        //    WindDirection = wdir;
        //    WindSpeed = wspd;
        //    WindPeakGust = wpgt;
        //    Pressure = pres;
        //    SunTime = tsun;

        //}
        //#endregion

        #region Methods

        //public string WeatherDataString()
        //{

        //    string s = string.Format($"" +
        //        $"{Date.ToString("yyyy/MM/dd"),3} " +
        //        $"{ AverageTemperature.ToString() + "°C",10} " +
        //        $"{ MinimumTemperature.ToString() + "°C",10} " +
        //        $"{ MinimumTemperature.ToString() + "°C",10} " +
        //        $"{ MaximumPrecipitation.ToString() + "%",10} " +
        //        $"{ Snow.ToString() + "cm",10} " +
        //        $"{ WindDirection.ToString() + "°",10} " +
        //        $"{ WindSpeed.ToString() + "km\\h",10} " +
        //        $"{ Pressure.ToString() + "mPa",12} " +
        //        $"{ SunTime.ToString() + "min",10}");

        //    return s;
        //}

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
