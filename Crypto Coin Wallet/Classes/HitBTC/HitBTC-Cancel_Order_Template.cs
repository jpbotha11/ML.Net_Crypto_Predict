using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Coin_Wallet
{
    class HitBTC_Cancel_Order_Template
    {
        public string id { get; set; }
        public string clientOrderId { get; set; }
        public string symbol { get; set; }
        public string side { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string timeInForce { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string cumQuantity { get; set; }
        public bool postOnly { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }


    }
}
