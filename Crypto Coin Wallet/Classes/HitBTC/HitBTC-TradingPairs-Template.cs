using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Coin_Wallet
{
    class HitBTC_TradingPairs_Template
    {
        public string id { get; set; }
        public string baseCurrency { get; set; }
        public string quoteCurrency { get; set; }
        public decimal quantityIncrement { get; set; }
        public decimal tickSize { get; set; }
        public decimal takeLiquidityRate { get; set; }
        public decimal provideLiquidityRate { get; set; }
        public string feeCurrency { get; set; }
    }
}
