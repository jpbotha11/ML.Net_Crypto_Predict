using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Coin_Wallet
{
    public class Ask
    {
        public string price { get; set; }
        public string size { get; set; }
    }

    public class Bid
    {
        public string price { get; set; }
        public string size { get; set; }
    }

    public class OrderBook
    {
        public List<Ask> ask { get; set; }
        public List<Bid> bid { get; set; }
    }
}
