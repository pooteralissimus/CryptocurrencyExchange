using DbAccessLibrary.DataAccess;
using DbAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary
{
	public static class CryptocurrencyOperations
	{

		public static List<dynamic> GetPrices(string[] coinsName, MyDbContext ctx)
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
				if(date.Hour == 0)
					DayOpenCheck(coinsPrice, ctx);

			return coinsPrice;
		}

		public static double Get24hChanges(string coinName, decimal coinPrice, MyDbContext ctx)  //24h changes calculate in percents
		{
			double result = 0;

			var dayOpenPrice = ctx.OpenPrices.Where(x => x.CoinName == coinName).Single().OpenPrice;
			double tmp = (double)(coinPrice - dayOpenPrice);
			result = (tmp * 100) / (double)coinPrice;
			return result;
		}

		public static void DayOpenCheck(List<dynamic> coinsPrice, MyDbContext ctx) //fix later
		{
			var date = DateTime.Now;
				var deletes = ctx.OpenPrices;
				foreach (var delete in deletes)
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
