using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Coin_Wallet
{
    class methods
    {
        public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public long DateTime_To_Unix(DateTime dt)
        {
            long unixTime = ((DateTimeOffset)dt).ToUnixTimeSeconds();
            return unixTime;
        }  

        public void Pull_Coin_History_NO_DB(string coin_symbol, string currency, bool csv_save)
        {
            //variables
            methods meth = new methods();
            List<CryptoCompare_Coin_History_Template.Datum> data_obj = new List<CryptoCompare_Coin_History_Template.Datum>();
            bool run = true;
            DateTime dt_earliest = DateTime.Now;
            CryptoCompare_API crypt_api = new CryptoCompare_API();

            //get btc

            //get btc price
            List<CryptoCompare_Coin_History_Template.Datum> btc_price = Pull_Coin_History_Structure("BTC", "ZAR");

            while (run)
            {
                //get history
                CryptoCompare_Coin_History_Template.RootObject hist = new CryptoCompare_Coin_History_Template.RootObject();
                hist = crypt_api.GetCoinHistory(coin_symbol, currency, 2000, dt_earliest.ToString("yyyy-MM-dd"));

                //null check
                if (hist.Data == null)
                {
                    return;
                }

                //check null
                if (hist.Data.Count == 0)
                {
                    return;
                }

                //convert die datums
                DateTime date_early = meth.UnixTimeStampToDateTime(double.Parse(hist.TimeFrom.ToString()));
                DateTime date_last = meth.UnixTimeStampToDateTime(double.Parse(hist.TimeTo.ToString()));

                //kyk if daar nog data is
                if (date_early == dt_earliest)
                {
                    break;
                }


                //set date
                for (int i = 0; i < hist.Data.Count; i++)
                {
                    //set date
                    hist.Data[i].s_date = meth.UnixTimeStampToDateTime(double.Parse(hist.Data[i].time.ToString()));

                    //add na die main list
                    data_obj.Add(hist.Data[i]);
                }

                //get the earliest date
                hist.Data.OrderBy(a => a.s_date);

                //moet weer run
                dt_earliest = hist.Data[0].s_date;

                //order die main list
                data_obj.OrderBy(a => a.s_date);

                //check if need to run again
                if (hist.Data.Count < 2000)
                {
                    break;
                }
            }

            //process history
            string csv = "";
            string csv_header = "";
            if (coin_symbol == "BTC")
            {
                csv_header = "close,high,date,low,open,volume_from,volume_to,symbol";
            }
            else
            {
                csv_header = "close,high,date,low,open,volume_from,volume_to,symbol,btc_price";
            }

            //string csv_header = "close,high,date,low,open,volume_from,volume_to,symbol";
            //string csv_header = "close,high,date,low,open,volume_from,volume_to,symbol,btc_price";

            //add the header
            csv += csv_header + Environment.NewLine;

            //loop
            foreach (CryptoCompare_Coin_History_Template.Datum item in data_obj)
            {
                //get btc price
                var btc_p = btc_price.Where(a => a.s_date == item.s_date).FirstOrDefault();

                //unix convert
                DateTime dates = meth.UnixTimeStampToDateTime(item.time);


                //build csv line
                string csv_line = item.close + "," + item.high + "," + dates.ToString("yyyy-MM-dd") + "," + item.low + "," + item.open
                    + "," + item.volumefrom + "," + item.volumeto + "," + coin_symbol + "," + btc_p.high;
                csv += csv_line + Environment.NewLine;
            }

            //save file
            if (csv_save)
            {
                System.IO.File.WriteAllText(@"C:\crypto_data\" + coin_symbol + ".csv", csv);
            }

        }

        public List<CryptoCompare_Coin_History_Template.Datum> Pull_Coin_History_Structure(string coin_symbol, string currency)
        {

            //variables
            methods meth = new methods();
            List<CryptoCompare_Coin_History_Template.Datum> data_obj = new List<CryptoCompare_Coin_History_Template.Datum>();
            bool run = true;
            DateTime dt_earliest = DateTime.Now;
            CryptoCompare_API crypt_api = new CryptoCompare_API();


            while (run)
            {
                //get history
                CryptoCompare_Coin_History_Template.RootObject hist = new CryptoCompare_Coin_History_Template.RootObject();
                hist = crypt_api.GetCoinHistory(coin_symbol, currency, 2000, dt_earliest.ToString("yyyy-MM-dd"));

                //convert die datums
                DateTime date_early = meth.UnixTimeStampToDateTime(double.Parse(hist.TimeFrom.ToString()));
                DateTime date_last = meth.UnixTimeStampToDateTime(double.Parse(hist.TimeTo.ToString()));

                //kyk if daar nog data is
                if (date_early == dt_earliest)
                {
                    break;
                }


                //set date
                for (int i = 0; i < hist.Data.Count; i++)
                {
                    //set date
                    hist.Data[i].s_date = meth.UnixTimeStampToDateTime(double.Parse(hist.Data[i].time.ToString()));

                    //add na die main list
                    data_obj.Add(hist.Data[i]);
                }

                //get the earliest date
                hist.Data.OrderBy(a => a.s_date);

                //moet weer run
                dt_earliest = hist.Data[0].s_date;

                //order die main list
                data_obj.OrderBy(a => a.s_date);

                //check if need to run again
                if (hist.Data.Count < 2000)
                {
                    break;
                }
            }
            return data_obj;
        }

    }
}
