using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Coin_Wallet
{
    class getBalancesResponse
    {

        public string currency { get; set; }
        public decimal available { get; set; }
        public decimal reserved { get; set; }


    }
}
