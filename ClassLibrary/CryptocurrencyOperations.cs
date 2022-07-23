using ClassLibrary.Models;
using CryptocurrencyExchange.Models;
using DbAccessLibrary.DataAccess;
using DbAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class CryptocurrencyOperations
    {

        public static List<CoinOutputModel> GetPrices(string[] coinsName, MyDbContext ctx)
        {
            List<CoinOutputModel> coinsList = new List<CoinOutputModel>();
            string json;

            Parallel.ForEach(coinsName, coinName =>
            {
                using (var web = new System.Net.WebClient())
                {
                    var url = $"https://api.binance.com/api/v3/ticker/price?symbol={coinName}USDT";
                    json = web.DownloadString(url);

                    dynamic coinInfo = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                
                        var coin = new CoinOutputModel()
                        {
                            Price = Convert.ToDecimal(coinInfo.price),
                            Name = coinName,
                        };
                        coinsList.Add(coin);
                }
            });

            foreach(var coin in coinsList)
            {
                coin.DayOpenPrice = ctx.OpenPrices.Where(x => x.CoinName == coin.Name).Single().OpenPrice;
                 coin.Changes24h = Get24hChanges(coin.Name, Convert.ToDecimal(coin.Price), coin.DayOpenPrice, ctx);
            }

            var date = DateTime.Now; //check for day open to save prices
            if (date.Hour == 0)
            {
                List<CryptoOpenPrices> openDayPrices = new List<CryptoOpenPrices>();
                foreach (var coin in coinsList)
                    openDayPrices.Add(new CryptoOpenPrices() { CoinName = coin.Name, OpenPrice = coin.Price });
                //  SaveDayOpen(openDayPrices, ctx);
            }
            coinsList = coinsList.OrderByDescending(x=> x.Price).ToList();
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
            var deletes = ctx.OpenPrices; //delete old coins price
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

        public static void Sell(string coinName, decimal coinPrice, decimal quantity, string userId, MyDbContext ctx)
        {
            var usdtQuantity = coinPrice * quantity;
            var accountBalance = ctx.AccountsBalance.Where(x => x.UserId == userId).ToList();
            accountBalance.Where(x => x.CoinName == "USDT").Single().Quantity += usdtQuantity;
            accountBalance.Where(x => x.CoinName == coinName).Single().Quantity -= quantity;
            ctx.SaveChanges();
        }

        public static void Send(string name, decimal coinPrice, decimal quantity, string receiverId, string currentUserId, MyDbContext ctx)
        {

            var coinsFrom = ctx.AccountsBalance.Where(x => x.UserId == currentUserId && x.CoinName == name).Single();
            var coinsTo = ctx.AccountsBalance.Where(x => x.UserId == receiverId && x.CoinName == name).SingleOrDefault();

            coinsFrom.Quantity -= quantity;

            //1 usd fee
            var feeUsdt = ctx.AccountsBalance.Where(x => x.UserId == "955e0897-59b9-4b78-a7d0-24feafa33f58" && x.CoinName == "USDT").Single(); //exchange usdt address 
            decimal oneUsd = 1;
            decimal oneUsdToCoin = oneUsd / coinPrice; //calculate current coin to 1 usd
            quantity -= oneUsdToCoin;
            feeUsdt.Quantity += oneUsd;

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
            ctx.SaveChanges();

        }



    }

}
