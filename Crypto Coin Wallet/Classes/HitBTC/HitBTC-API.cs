using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using RestSharp;

namespace Crypto_Coin_Wallet
{
    class HitBTC_API
    {
        string api_key = "";
        string api_secret = "";
        int max_trade_days = 3;
        int trade_history_days = 7;

        public List<HitBTC_MarketData_Template.Coin> getCurrencies()
        {
            http web = new http();
            List<HitBTC_MarketData_Template.Coin> lst_coins = new List<HitBTC_MarketData_Template.Coin>();

            string json = web.jsonGet("https://api.hitbtc.com/api/2/public/currency");

            //to json
            var market_data = JsonConvert.DeserializeObject<dynamic>(json);

            foreach (var item in market_data)
            {
                string id = item.id;
                string fullName = item.fullName;
                bool crypto = item.crypto;
                bool payinEnabled = item.payinEnabled;
                bool payinPaymentId = item.payinPaymentId;
                int payinConfirmations = int.Parse(item.payinConfirmations.ToString());
                bool payoutEnabled = item.payoutEnabled;
                bool payoutIsPaymentId = item.payoutIsPaymentId;
                bool transferEnabled = item.transferEnabled;
                bool delisted = item.delisted;
                string payoutFee = item.payoutFee;


                HitBTC_MarketData_Template.Coin coin = new HitBTC_MarketData_Template.Coin();
                coin.id = id;
                coin.fullName = fullName;
                coin.crypto = crypto;
                coin.payinEnabled = payinEnabled;
                coin.payinPaymentId = payinPaymentId;
                coin.payinConfirmations = payinConfirmations;
                coin.payoutEnabled = payoutEnabled;
                coin.payoutIsPaymentId = payoutIsPaymentId;
                coin.transferEnabled = transferEnabled;
                coin.delisted = delisted;
                coin.payoutFee = payoutFee;

                lst_coins.Add(coin);

            }

            //final return
            return lst_coins;
        }

        public List<HitBTC_ActiveOrders_Template> getActiveOrders()
        {
            //variables
            List<HitBTC_ActiveOrders_Template> lst_active_orders = new List<HitBTC_ActiveOrders_Template>();

            //get json
            string json = jsonGetAuth("/api/2/order");

            //parse
            var market_data = JsonConvert.DeserializeObject<dynamic>(json);

            foreach (var active_order in market_data)
            {
                HitBTC_ActiveOrders_Template order = new HitBTC_ActiveOrders_Template();
                order.clientOrderId = active_order.clientOrderId;
                order.createdAt = active_order.createdAt;
                order.cumQuantity = decimal.Parse(active_order.cumQuantity.ToString());
                order.id = active_order.id;
                order.price = decimal.Parse(active_order.price.ToString());
                order.quantity = decimal.Parse(active_order.quantity.ToString());
                order.side = active_order.side;
                order.status = active_order.status;
                order.symbol = active_order.symbol;
                order.timeInForce = active_order.timeInForce;
                order.type = active_order.type;
                order.updatedAt = active_order.updatedAt;

                //add to list
                lst_active_orders.Add(order);
            }

            return lst_active_orders;
        }

        public List<getBalancesResponse> getBalances()
        {

            //get json
            string json = jsonGetAuth("/api/2/trading/balance");

            //parse
            var market_data = JsonConvert.DeserializeObject<dynamic>(json);

            //loop
            List<getBalancesResponse> lst_balances = new List<getBalancesResponse>();
            foreach (var coin in market_data)
            {
                getBalancesResponse curr = new getBalancesResponse();
                curr.available = decimal.Parse(coin.available.ToString());
                curr.currency = coin.currency;
                curr.reserved = decimal.Parse(coin.reserved.ToString());

                //add
                if (curr.available > 0)
                {
                    lst_balances.Add(curr);
                }

            }
            return lst_balances;
        }

        public List<HitBTC_GetTicker_Template> getTickers()
        {
            //get json
            string json = jsonGetAuth("/api/2/public/ticker");

            //parse
            var market_data = JsonConvert.DeserializeObject<dynamic>(json);
            List<HitBTC_GetTicker_Template> lst_tickers = new List<HitBTC_GetTicker_Template>();
            foreach (var ticker in market_data)
            {
                HitBTC_GetTicker_Template tick = new HitBTC_GetTicker_Template();

                if (ticker.ask.ToString() == null || ticker.ask.ToString() == "")
                {
                    tick.ask = 0;
                }
                else
                {
                    tick.ask = decimal.Parse(ticker.ask.ToString());
                }


                if (ticker.bid.ToString() == null || ticker.bid.ToString() == "")
                {
                    tick.bid = 0;
                }
                else
                {
                    tick.bid = decimal.Parse(ticker.bid.ToString());
                }

                if (ticker.high.ToString() == null || ticker.high.ToString() == "")
                {
                    tick.high = 0;
                }
                else
                {
                    tick.high = decimal.Parse(ticker.high.ToString());
                }

                if (ticker.last.ToString() == null || ticker.last.ToString() == "")
                {
                    tick.last = 0;
                }
                else
                {
                    tick.last = decimal.Parse(ticker.last.ToString());
                }

                if (ticker.low.ToString() == null || ticker.low.ToString() == "")
                {
                    tick.low = 0;
                }
                else
                {
                    tick.low = decimal.Parse(ticker.low.ToString());
                }

                if (ticker.open.ToString() == null || ticker.open.ToString() == "")
                {
                    tick.open = 0;
                }
                else
                {
                    tick.open = decimal.Parse(ticker.open.ToString());
                }


                tick.symbol = ticker.symbol;
                tick.timestamp = ticker.timestamp;

                if (ticker.volume.ToString() == null || ticker.volume.ToString() == "")
                {
                    ticker.volume = 0;
                }
                else
                {
                    tick.volume = decimal.Parse(ticker.volume.ToString());
                }

                if (ticker.volumeQuote.ToString() == null || ticker.volumeQuote.ToString() == "")
                {
                    tick.volumeQuote = 0;
                }
                else
                {
                    tick.volumeQuote = decimal.Parse(ticker.volumeQuote.ToString());
                }



                //add to list
                lst_tickers.Add(tick);
            }

            return lst_tickers;

        }

        public List<HitBTC_TradingPairs_Template> getATradingPair(string coin)
        {
            List<HitBTC_TradingPairs_Template> lst = getTradingPairs();

            List<HitBTC_TradingPairs_Template> lst_pair = new List<HitBTC_TradingPairs_Template>();
            foreach (HitBTC_TradingPairs_Template pair in lst)
            {
                if (pair.baseCurrency == coin || pair.quoteCurrency == coin)
                {
                    lst_pair.Add(pair);
                }
            }

            return lst_pair;
        }

        public List<HitBTC_TradingPairs_Template> getTradingPairs()
        {
            //get json
            string json = jsonGetAuth("/api/2/public/symbol");

            //parse
            var market_data = JsonConvert.DeserializeObject<dynamic>(json);

            List<HitBTC_TradingPairs_Template> lst_pairs = new List<HitBTC_TradingPairs_Template>();
            foreach (var pair in market_data)
            {
                HitBTC_TradingPairs_Template template = new HitBTC_TradingPairs_Template();
                template.baseCurrency = pair.baseCurrency;
                template.feeCurrency = pair.feeCurrency;
                template.id = pair.id;
                template.provideLiquidityRate = decimal.Parse(pair.provideLiquidityRate.ToString());
                template.quantityIncrement = decimal.Parse(pair.quantityIncrement.ToString());
                template.quoteCurrency = pair.quoteCurrency;
                template.takeLiquidityRate = decimal.Parse(pair.takeLiquidityRate.ToString());
                template.tickSize = decimal.Parse(pair.tickSize.ToString());
                lst_pairs.Add(template);
            }

            return lst_pairs;
        }

        public OrderBook getOrderBook(string trade_pair)
        {
            //get json
            string json = jsonGetAuth("/api/2/public/orderbook/" + trade_pair);

            //parse
            OrderBook market_data = JsonConvert.DeserializeObject<OrderBook>(json);

            return market_data;



        }

        public List<HitBTC_GetCandles_Template> getCandles(string trade_pair, int days)
        {

            //get json
            string json = jsonGetAuth("/api/2/public/candles/" + trade_pair + "?period=D1");

            //parse
            var market_data = JsonConvert.DeserializeObject<dynamic>(json);

            //loop
            List<HitBTC_GetCandles_Template> lst_candles = new List<HitBTC_GetCandles_Template>();
            foreach (var coin in market_data)
            {
                HitBTC_GetCandles_Template template = new HitBTC_GetCandles_Template();
                template.timestamp = DateTime.Parse(coin.timestamp.ToString());
                template.close = coin.close;
                template.max = coin.max;
                template.min = coin.min;
                template.open = coin.open;
                template.volume = coin.volume;
                template.volumeQuote = coin.volumeQuote;
                lst_candles.Add(template);


            }
            return lst_candles;
        }

        public HitBTC_OrderBook_Average_Template getOrderAverages(string trade_pair)
        {
            //variables
            HitBTC_OrderBook_Average_Template avg = new HitBTC_OrderBook_Average_Template();

            OrderBook orders = getOrderBook(trade_pair);

            //avg die ask 
            decimal ask_price_total = 0;
            foreach (Ask ask_item in orders.ask)
            {
                ask_price_total += decimal.Parse(ask_item.price.ToString());
            }
            ask_price_total = (ask_price_total / orders.ask.Count());
            avg.ask_price_average = ask_price_total;


            decimal bid_price_total = 0;
            foreach (Bid bid_item in orders.bid)
            {
                bid_price_total += decimal.Parse(bid_item.price.ToString());
            }
            bid_price_total = (bid_price_total / orders.bid.Count());
            avg.bid_price_average = bid_price_total;

            return avg;
        }

        public HitBTC_Cancel_Order_Template CancelOrder(string orderID)
        {
            //get json
            string json = jsonDelete("/api/2/order/" + orderID);

            //do
            HitBTC_Cancel_Order_Template resp = JsonConvert.DeserializeObject<HitBTC_Cancel_Order_Template>(json);

            return resp;

        }

        public string jsonGetAuth(string url)
        {
            var client = new RestClient("https://api.hitbtc.com")
            {
                Authenticator = new HttpBasicAuthenticator(api_key, api_secret)
            };

            var request = new RestRequest(url, Method.GET)
            {
                RequestFormat = DataFormat.Json
            };

            var response = client.Execute(request);
            return response.Content;

        }

        public string jsonDelete(string url)
        {
            var client = new RestClient("https://api.hitbtc.com")
            {
                Authenticator = new HttpBasicAuthenticator(api_key, api_secret)
            };

            var request = new RestRequest(url, Method.DELETE)
            {
                RequestFormat = DataFormat.Json
            };

            var response = client.Execute(request);
            return response.Content;

        }

    
    }
}
