using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Coin_Wallet
{
    class HitBTC_MarketData_Template
    {
        public class Coin
        {
            public string id { get; set; }
            public string fullName { get; set; }
            public bool crypto { get; set; }
            public bool payinEnabled { get; set; }
            public bool payinPaymentId { get; set; }
            public int payinConfirmations { get; set; }
            public bool payoutEnabled { get; set; }
            public bool payoutIsPaymentId { get; set; }
            public bool transferEnabled { get; set; }
            public bool delisted { get; set; }
            public string payoutFee { get; set; }
        }

    }
}
