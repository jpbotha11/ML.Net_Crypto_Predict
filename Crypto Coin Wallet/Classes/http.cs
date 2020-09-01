using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Crypto_Coin_Wallet
{
    class http
    {
        private string getAllCoinsHTTP()
        {
            string responseBody = String.Empty;
            //string call_url = "https://www.cryptocompare.com/api/data/coinlist/"; //crypto compare
            string call_url = "https://www.cryptopia.co.nz/api/GetCurrencies"; //cryptopia

            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //ServicePointManager.DefaultConnectionLimit = 9999;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(call_url);
            request.ContentType = "application/json";
            request.Method = "GET";
            request.Timeout = 100000;
            try
            {
                Console.WriteLine("Submitting Request");
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    //return status code
                    Console.WriteLine("Status Code: {1} {0}", ((HttpWebResponse)response).StatusCode, (int)((HttpWebResponse)response).StatusCode);
                }
                //Console.Out.WriteLine(responseBody);
            }
            catch (WebException ex)
            {
                return "-1";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }
            return responseBody;

        }

        private string getUSD_ZARHTTP()
        {
            string responseBody = String.Empty;
            //string call_url = "https://www.cryptocompare.com/api/data/coinlist/"; //crypto compare
            string call_url = "https://free.currencyconverterapi.com/api/v5/convert?q=USD_ZAR"; //cryptopia

            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //ServicePointManager.DefaultConnectionLimit = 9999;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(call_url);
            request.ContentType = "application/json";
            request.Method = "GET";
            request.Timeout = 100000;
         
            try
            {
                Console.WriteLine("Submitting Request");
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    //return status code
                    Console.WriteLine("Status Code: {1} {0}", ((HttpWebResponse)response).StatusCode, (int)((HttpWebResponse)response).StatusCode);
                }
                //Console.Out.WriteLine(responseBody);
            }
            catch (WebException ex)
            {
                return "-1";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }
            return responseBody;

        }

        private string getCoinPriceHTTP(string coin_symbol)
        {
            string responseBody = String.Empty;
            string call_url = "https://min-api.cryptocompare.com/data/price?fsym=" + coin_symbol + "&tsyms=USD,ZAR";

            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //ServicePointManager.DefaultConnectionLimit = 9999;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(call_url);
            request.ContentType = "application/json";
            request.Method = "GET";
            request.Timeout = 100000;
       
            try
            {
                Console.WriteLine("Submitting Request");
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    //return status code
                    Console.WriteLine("Status Code: {1} {0}", ((HttpWebResponse)response).StatusCode, (int)((HttpWebResponse)response).StatusCode);
                }
                Console.Out.WriteLine(responseBody);
            }
            catch (WebException ex)
            {
                return "-1";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }
            return responseBody;

        }

        private coinsTemplatecs.RootObject parseAllCoins()
        {
            //get json
            string json = getAllCoinsHTTP();

            //coins class
            coinsTemplatecs cT = new coinsTemplatecs();
            coinsTemplatecs.RootObject allCoins = cT.parseJsonCoins(json);

            //return
            return allCoins;
        }

        public coinsTemplatecs.RootObject getAllCoins()
        {
            return parseAllCoins();
        }

        public string getAllCoinsJson()
        {
            return getAllCoinsHTTP();
        }

        public coinPrice.RootObject getCoinPrice(string coin_symbol)
        {
            try
            {
                //kry json
                string json = getCoinPriceHTTP(coin_symbol);

                //parse
                coinPrice cp = new coinPrice();
                coinPrice.RootObject coinPrice = cp.parseCoinsPrice(json);

                //return
                return coinPrice;
            }
            catch (Exception)
            {
                return null;    
            }
           
        }

        public USD_ZAR.RootObject getUSD_ZAR_Exchange()
        {
            try
            {
                //json
                string json = getUSD_ZARHTTP();

                //parse
                USD_ZAR ex = new USD_ZAR();
                USD_ZAR.RootObject zar_exchange = ex.parseUSD_ZAR(json);

                return zar_exchange;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        //Coin Price History
        private string getCoinPriceHistory(string coin_symbol, string output_symbol)
        {
            string responseBody = String.Empty;         
            string call_url = "https://min-api.cryptocompare.com/data/histoday?fsym=" + coin_symbol +"&tsym="+ output_symbol + "&limit=30&aggregate=1&e=CCCAGG";      

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(call_url);
            request.ContentType = "application/json";
            request.Method = "GET";
            request.Timeout = 100000;           
            try
            {
                Console.WriteLine("Submitting Request");
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    //return status code
                    Console.WriteLine("Status Code: {1} {0}", ((HttpWebResponse)response).StatusCode, (int)((HttpWebResponse)response).StatusCode);
                }
                //Console.Out.WriteLine(responseBody);
            }
            catch (WebException ex)
            {
                return "-1";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }
            return responseBody;

        }

        public string jsonGet(string url)
        {
            string responseBody = String.Empty;
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //ServicePointManager.DefaultConnectionLimit = 9999;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";
            request.Timeout = 100000;
            try
            {
              
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();                 
                }
               
            }
            catch (WebException ex)
            {
                return "-1";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }
            return responseBody;

        }
        public string jsonGetAuth(string url)
        {
            string responseBody = String.Empty;
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //ServicePointManager.DefaultConnectionLimit = 9999;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";
            request.Timeout = 100000;
            try
            {

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    responseBody = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                }

            }
            catch (WebException ex)
            {
                return "-1";
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex.Message);
            }
            return responseBody;

        }
    }
}
