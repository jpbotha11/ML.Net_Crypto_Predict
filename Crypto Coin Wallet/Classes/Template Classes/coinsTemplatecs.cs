using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crypto_Coin_Wallet
{
    public class coinsTemplatecs
    {
        public RootObject parseJsonCoins(string json)
        {
            RootObject obj = JsonConvert.DeserializeObject<RootObject>(json);
            return obj;
        }

        public class Coin
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Symbol { get; set; }
            public string Algorithm { get; set; }
            public double WithdrawFee { get; set; }
            public double MinWithdraw { get; set; }
            public double MaxWithdraw { get; set; }
            public double MinBaseTrade { get; set; }
            public bool IsTipEnabled { get; set; }
            public double MinTip { get; set; }
            public int DepositConfirmations { get; set; }
            public string Status { get; set; }
            public string StatusMessage { get; set; }
            public string ListingStatus { get; set; }
        }

        public class RootObject
        {
            public bool Success { get; set; }
            public object Message { get; set; }
            public List<Coin> Data { get; set; }
            public object Error { get; set; }
        }
    }
}