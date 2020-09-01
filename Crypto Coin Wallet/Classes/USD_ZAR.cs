using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crypto_Coin_Wallet
{
    class USD_ZAR
    {
        public RootObject parseUSD_ZAR(string json)
        {
            RootObject obj = JsonConvert.DeserializeObject<RootObject>(json);
            return obj;
        }


        public class Query
        {
            public int count { get; set; }
        }

        public class USDZAR
        {
            public string id { get; set; }
            public double val { get; set; }
            public string to { get; set; }
            public string fr { get; set; }
        }

        public class Results
        {
            public USDZAR USD_ZAR { get; set; }
        }

        public class RootObject
        {
            public Query query { get; set; }
            public Results results { get; set; }
        }
    }
}
