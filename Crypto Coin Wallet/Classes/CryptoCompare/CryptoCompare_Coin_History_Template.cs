﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Coin_Wallet
{
    public class CryptoCompare_Coin_History_Template
    {
        public class Datum
        {
            public int time { get; set; }
            public decimal close { get; set; }
            public decimal high { get; set; }
            public decimal low { get; set; }
            public decimal open { get; set; }
            public decimal volumefrom { get; set; }
            public decimal volumeto { get; set; }
            public DateTime s_date { get; set; }
        }

        public class ConversionType
        {
            public string type { get; set; }
            public string conversionSymbol { get; set; }
        }

        public class RateLimit
        {
        }

        public class RootObject
        {
            public string Response { get; set; }
            public int Type { get; set; }
            public bool Aggregated { get; set; }
            public List<Datum> Data { get; set; }
            public int TimeTo { get; set; }
            public int TimeFrom { get; set; }
            public bool FirstValueInArray { get; set; }
            public ConversionType ConversionType { get; set; }
            public RateLimit RateLimit { get; set; }
            public bool HasWarning { get; set; }
        }

    }
}
