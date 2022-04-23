using DbAccessLibrary.DataAccess;
using DbAccessLibrary.Models;
using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public static class CryptocurrencyOperations
    {

        public static List<dynamic> GetPrices(string[] coinsName,MyDbContext ctx)
        {
            List<dynamic> coinsPrice = new List<dynamic>();
            string json;

            foreach (var coinName in coinsName)
            {
                using (var web = new System.Net.WebClient())
                {
                    var url = $"https://api.binance.com/api/v3/ticker/price?symbol={coinName}USDT";
                    json = web.DownloadString(url);
                }

                dynamic coinInfo = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                coinsPrice.Add(coinInfo);
            }

            var date = DateTime.Now; //check for day open to save prices
            if (date.Hour == 0) {
                DayOpenCheck(coinsPrice, ctx);
            }

                return coinsPrice;

        }

        public static double Get24hChanges(string coinName, MyDbContext ctx)
        {
            double result = 0;

            return result;
        }

        public static void DayOpenCheck(List<dynamic> coinsPrice, MyDbContext ctx) //fix later
        {
            var date = DateTime.Now;
            if (date.Hour == 0)
            {
                var deletes = ctx.OpenPrices;
               foreach(var delete in deletes)
                    ctx.OpenPrices.Remove(delete);


                foreach (var coin in coinsPrice)
                {
                    string coinname = coin.symbol;
                    coinname = coinname.Remove(coinname.Length - 4);
                    var openPrice = new CryptoOpenPrices() { CoinName = coinname, OpenPrice = Convert.ToDecimal(coin.price) };
                    ctx.OpenPrices.Add(openPrice);
                }
                 ctx.SaveChanges();
            }
        }
    }

}
