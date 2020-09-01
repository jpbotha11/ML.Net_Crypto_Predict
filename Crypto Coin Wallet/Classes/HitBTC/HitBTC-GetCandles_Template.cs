using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Coin_Wallet
{
    class HitBTC_GetCandles_Template
    {
        public DateTime timestamp { get; set; }
        public string open { get; set; }
        public string close { get; set; }
        public string min { get; set; }
        public decimal max { get; set; }
        public string volume { get; set; }
        public string volumeQuote { get; set; }


    }
}
