using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Coin_Wallet
{

   
    /*
     * 
     * 
     * "id": 840450210,
    "clientOrderId": "c1837634ef81472a9cd13c81e7b91401",
    "symbol": "ETHBTC",
    "side": "buy",
    "status": "partiallyFilled",
    "type": "limit",
    "timeInForce": "GTC",
    "quantity": "0.020",
    "price": "0.046001",
    "cumQuantity": "0.005",
    "createdAt": "2017-05-12T17:17:57.437Z",
    "updatedAt": "2017-05-12T17:18:08.610Z"*/


    class HitBTC_ActiveOrders_Template
    {
        public string id { get; set; }
        public string clientOrderId { get; set; }
        public string symbol { get; set; }
        public string side { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public string timeInForce { get; set; }
        public decimal quantity { get; set; }
        public decimal price { get; set; }
        public decimal cumQuantity { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}
