using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Coin_Wallet
{
    class CryptoCompare_API
    {
        string api_key = "";
        
        public CryptoCompare_Coin_History_Template.RootObject GetCoinHistory(string coin_lookup, string currency, int limit, string from_date)
        {
            //variables
            methods meth = new methods();
            string hour_url = "https://min-api.cryptocompare.com/data/histohour?fsym=" + coin_lookup + "&tsym=" + currency + "&limit=" + limit.ToString();            
            string day_url = "https://min-api.cryptocompare.com/data/histoday?fsym=" + coin_lookup + "&tsym=" + currency + "&limit=" + limit.ToString();
            string call_url = "";

            //check for dates
            if (string.IsNullOrWhiteSpace(from_date))
            {
                //no offset
                call_url = day_url;

            }
            else
            {
                //kry date
                DateTime dt_from = DateTime.Parse(from_date);

                //convert date
                double dt_unix = meth.DateTime_To_Unix(dt_from);
                call_url = day_url + "&toTs=" + dt_unix;
            }

            //get the json
            http http_class = new http();
            string json = http_class.jsonGet(day_url);

            //parse
            CryptoCompare_Coin_History_Template.RootObject coin_data;
            try
            {
                coin_data = JsonConvert.DeserializeObject<CryptoCompare_Coin_History_Template.RootObject>(json);
            }
            catch (Exception)
            {

                coin_data = new CryptoCompare_Coin_History_Template.RootObject();
                return coin_data;
            }
          

            //return parse data
            return coin_data;
        }     
       
    }
}
