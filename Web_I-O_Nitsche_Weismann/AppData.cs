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
        string _size;
        string _installs;
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
                if (value > 0)
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
                if (value > 0)
                {
                    _reviews = value;
                }
                else
                {
                    throw new Exception("Invalid Reviews Value");
                }
            }
        }

        public string Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (value.Length > 0)
                {
                    _size = value;
                }
                else
                {
                    throw new Exception("Invalid Size Value");
                }
            }
        }

        public string Installs
        {
            get
            {
                return _installs;
            }
            set
            {
                if (value.Length > 1)
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
                if (!(value < 0))
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
                if (value < DateTime.Now)
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

        public AppData(string appName, int rating, int reviews, string size, string installs, double price, DateTime lastUpdated, string currentVersion, string androidVersion, myEnums.Category category, myEnums.PriceType priceType, myEnums.ContentRating contentRating, myEnums.Genres genres)
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
                $"{ Category.ToString(), 20} " +
                $"{ Rating.ToString(), 3} " +
                $"{ Reviews.ToString(), 10} " +
                $"{ Size, 10} " +
                $"{ Installs.ToString(),10} " +
                $"{ PriceType.ToString(), 8} " +
                $"{ ContentRating.ToString(), 12} " +
                $"{ Genres.ToString(), 20} " +
                $"{ LastUpdated.ToString("dd.MM.yyyy"), 12} " +
                $"{ CurrentVersion.ToString(), 10} " +
                $"{ AndroidVersion.ToString(), 10}");

            return s;
        }

        #endregion
    }
}
