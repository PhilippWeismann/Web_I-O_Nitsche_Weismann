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
            WEATHER,
            HEALTH_AND_FITNESS,
            PHOTOGRAPHY
        }
        public enum Type
        {
            Free,
            Paid
        }
        public enum Content
        {
            Everyone ,
            Everyone_10,        //Everyone 10+      //mit switch case schauen ob der string Everyone 10+ ist und in daun dieses enum auswählen 
            Teen,
            Mature_17           //Mature 17         //mit switch case schauen ob der string Mature 17 ist und in daun dieses enum auswählen
        }
        public enum Genres
        {
            Weather,
            Health_Fitness,     //Health & Fitness  //mit switch case schauen ob der string Health & Fitness ist und in daun dieses enum auswählen
            Photography
        }
    }
}
