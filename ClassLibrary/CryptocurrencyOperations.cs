using ClassLibrary.Models;
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
                    Changes24h = Get24hChanges(coinName, Convert.ToDecimal(coinInfo.price), -1, ctx)
                };

                coinsList.Add(coin);
            }

            var date = DateTime.Now; //check for day open to save prices
            if (date.Hour == 0)
            {
                List<CryptoOpenPrices> openDayPrices = new List<CryptoOpenPrices>();
                foreach (var coin in coinsList)
                    openDayPrices.Add(new CryptoOpenPrices() { CoinName = coin.Name, OpenPrice = coin.Price });
                //  SaveDayOpen(openDayPrices, ctx);
            }

            return coinsList;
        }

        public static float Get24hChanges(string coinName, decimal coinPrice, decimal dayOpenPrice = -1, MyDbContext ctx = null)  //24h changes calculate in percents
        {
            float result = 0;

            if (dayOpenPrice == -1)
                dayOpenPrice = ctx.OpenPrices.Where(x => x.CoinName == coinName).Single().OpenPrice;

            result = (float)(((coinPrice - dayOpenPrice) / dayOpenPrice) * 100);
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

        public static FuturesPositionOutput LongShort(FuturesData position, MyDbContext ctx)
        {
            var currentCoinInfo = GetPrices(new string[] { position.CoinName }, ctx).Single();
            var changes = Get24hChanges(position.CoinName, currentCoinInfo.Price, position.OpenPrice);
            changes *= position.Leverage;

            decimal usdtTotal = position.Usdt;
            decimal difference = (usdtTotal / 100) * (decimal)changes;

            if (position.LongShort == "short") difference *= -1; // short means price goes down so difference * -1

            usdtTotal += difference;
            FuturesPositionOutput output = new FuturesPositionOutput()
            {
                CoinName = position.CoinName,
                StartedTotal = position.Usdt,
                OpenPrice = position.OpenPrice,
                CurrentPrice = currentCoinInfo.Price,
                CurrentTotal = usdtTotal,
                PercentChanges = changes,
                Leverage = position.Leverage,
            };

            return output;
        }

        public static void Send(string name, decimal quantity, string receiverId, string currentUserId, MyDbContext ctx)
        {
            var coinsFrom = ctx.AccountsBalance.Where(x => x.UserId == currentUserId && x.CoinName == name).Single();

            var coinsTo = ctx.AccountsBalance.Where(x => x.UserId == receiverId && x.CoinName == name).SingleOrDefault();

            if (coinsTo == null)
            {
                coinsTo = new AccountBalance()
                {
                    CoinName = name,
                    Quantity = quantity,
                    UserId = receiverId
                };
                ctx.AccountsBalance.Add(coinsTo);
            }
            else
            {
                coinsTo.Quantity += quantity;
            }
            coinsFrom.Quantity -= quantity;
            ctx.SaveChanges();

        }



    }

}
