using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Coin_Wallet
{
    class HitBTC_Coin_History_Template
    {

        public class Rootobject
        {
            public Class1[] Property1 { get; set; }
        }

        public class Class1
        {
            public DateTime timestamp { get; set; }
            public string open { get; set; }
            public string close { get; set; }
            public string min { get; set; }
            public string max { get; set; }
            public string volume { get; set; }
            public string volumeQuote { get; set; }
        }


    }

}