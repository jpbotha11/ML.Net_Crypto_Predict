using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Coin_Wallet
{
    class HitBTC_GetTicker_Template
    {
        /*
         * "ask": "0.050043",
    "bid": "0.050042",
    "last": "0.050042",
    "open": "0.047800",
    "low": "0.047052",
    "high": "0.051679",
    "volume": "36456.720",
    "volumeQuote": "1782.625000",
    "timestamp": "2017-05-12T14:57:19.999Z",
    "symbol": "ETHBTC"*/

        public decimal ask { get; set; }
        public decimal bid { get; set; }
        public decimal last { get; set; }
        public decimal open { get; set; }
        public decimal low { get; set; }
        public decimal high { get; set; }
        public decimal volume { get; set; }
        public decimal volumeQuote { get; set; }
        public string timestamp { get; set; }
        public string symbol { get; set; }
    }
}
