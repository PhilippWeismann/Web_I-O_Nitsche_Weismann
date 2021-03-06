using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_I_O_Nitsche_Weismann
{
    class myEnums
    {
        public enum Category
        {
            Weather,
            Health_and_Fitness,
            Photography
        }
        public enum PriceType
        {
            Free,
            Paid
        }
        public enum ContentRating
        {
            Everyone ,
            Everyone_10,        //Everyone 10+      
            Teen,
            Mature_17           //Mature 17      
        }
        public enum Genres
        {
            Weather,
            Health_Fitness,     //Health & Fitness  
            Photography
        }
        public enum Filter
        {
            Price,
            Reviews,
            Size
        }

        public enum Operator
        {
            greater_or_equals,
            less_or_equals
        }

    }
}
