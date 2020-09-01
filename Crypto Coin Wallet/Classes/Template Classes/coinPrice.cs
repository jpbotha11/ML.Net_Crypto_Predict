using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crypto_Coin_Wallet
{
    class coinPrice
    {
        public RootObject parseCoinsPrice(string json)
        {
            RootObject obj = JsonConvert.DeserializeObject<RootObject>(json);
            return obj;
        }

        public class RootObject
        {
            public double USD { get; set; }
            public double ZAR { get; set; }
        }
    }
}
