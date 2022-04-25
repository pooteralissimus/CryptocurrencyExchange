using CryptocurrencyExchange.Models;
using DbAccessLibrary.DataAccess;
using DbAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary
{
    public static class CryptocurrencyOperations
    {

        public static List<CoinOutputModel> GetPrices(string[] coinsName, MyDbContext ctx)
        {
            List<CoinOutputModel> coinsList = new List<CoinOutputModel>();
            string json;

            foreach (var coinName in coinsName)
            {
                using (var web = new System.Net.WebClient())
                {
                    var url = $"https://api.binance.com/api/v3/ticker/price?symbol={coinName}USDT";
                    json = web.DownloadString(url);
                }

                dynamic coinInfo = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                var coin = new CoinOutputModel()
                {
                    Price = Convert.ToDecimal(coinInfo.price),
                    Name = coinName,
                    DayOpenPrice = ctx.OpenPrices.Where(x => x.CoinName == coinName).Single().OpenPrice,
                    Changes24h = Get24hChanges(coinName, Convert.ToDecimal(coinInfo.price), ctx)
                };

                coinsList.Add(coin);
            }

            var date = DateTime.Now; //check for day open to save prices
            if (date.Hour == 0) {
                List<CryptoOpenPrices> openDayPrices = new List<CryptoOpenPrices>();
                foreach(var coin in coinsList)
                    openDayPrices.Add(new CryptoOpenPrices() { CoinName = coin.Name, OpenPrice = coin.Price });
              }

            return coinsList;
        }

        public static double Get24hChanges(string coinName, decimal coinPrice, MyDbContext ctx)  //24h changes calculate in percents
        {
            double result = 0;

            var dayOpenPrice = ctx.OpenPrices.Where(x => x.CoinName == coinName).Single().OpenPrice;
            double tmp = (double)(coinPrice - dayOpenPrice);
            result = (tmp * 100) / (double)coinPrice;
            return result;
        }

        public static void SaveDayOpen(List<CryptoOpenPrices> opens, MyDbContext ctx) 
        {
            var deletes = ctx.OpenPrices;
            foreach (var delete in deletes)
                ctx.OpenPrices.Remove(delete);

            foreach (var coin in opens)
            {
                var openPrice = new CryptoOpenPrices() { CoinName = coin.CoinName, OpenPrice = coin.OpenPrice };
                ctx.OpenPrices.Add(openPrice);
            }
            ctx.SaveChanges();
        }

    }

}
