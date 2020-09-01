using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Cryptopia.API;
using Cryptopia.API.DataObjects;
using System.Threading.Tasks;
using Cryptopia.API.Models;

/*
 * 
 * NULL RESPONSES MOET NOG REGGEMAAK WORD!!
 * */



namespace Crypto_Coin_Wallet
{
    public partial class Form1 : Form
    {
        //globals
        coinsTemplatecs.RootObject allCoins = new coinsTemplatecs.RootObject();

        //previous coin price dic
        Dictionary<string, double> dicCoin_Last_Price = new Dictionary<string, double>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void logCoinHistory(string coin_symbol, double coin_price_ZAR)
        {
            //bou history lyn
            string history_line = coin_symbol + "," + coin_price_ZAR.ToString() + "," + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string path = coin_symbol + "txt";

            //eerste sien of die text value bestaan vir hierdie coin
            bool file_exist = System.IO.File.Exists(path);
            if (file_exist == false)
            {
                //create the file
                File.Create(path);
            }
            else
            {
                //skryf die nuwe data net by
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnHist_Click(object sender, EventArgs e)
        {

        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            //vars
            HitBTC_API api = new HitBTC_API();
            methods meth = new methods();

            //kry coins
            List<HitBTC_TradingPairs_Template> coins = api.getTradingPairs();

            //kry base currencies
            var basecurrencies = coins.Select(a => a.quoteCurrency).Distinct();

            //first get BTC price
            meth.Pull_Coin_History_NO_DB("BTC", "ZAR", true);

            //kry alle btc
            List<HitBTC_TradingPairs_Template> btc_curr = coins.Where(b => b.quoteCurrency == "BTC").ToList();

            //kry die history vir elkeen
            foreach (HitBTC_TradingPairs_Template coin in btc_curr)
            {
                //pull
                meth.Pull_Coin_History_NO_DB(coin.baseCurrency, "USD", true);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {

        }
    }

}
