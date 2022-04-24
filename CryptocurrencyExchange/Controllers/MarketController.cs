using ClassLibrary;
using CryptocurrencyExchange.Models;
using DbAccessLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptocurrencyExchange.Controllers
{
	public class MarketController : Controller
	{
		private readonly MyDbContext _context;
		public MarketController(MyDbContext context) { _context = context; }


		public IActionResult Index()
		{

			string[] coinsName = { "BTC","ETH", "XRP", "DOGE", "LUNA", 
				"SOL", "ATOM", "AXS", "MANA"};

			List<CoinOutputModel> outputCoins = new List<CoinOutputModel>();

			var prices = CryptocurrencyOperations.GetPrices(coinsName, _context);

			foreach (var price in prices)
			{
				string name = price.symbol;
				name = name.Remove(name.Length - 4); //remove usdt 
				var coin = new CoinOutputModel()
				{
					Name = name,
					Price = Convert.ToDecimal(price.price),
					DayOpenPrice = _context.OpenPrices.Where(x => x.CoinName == name).Single().OpenPrice,
				};

				coin.changes24h = CryptocurrencyOperations.Get24hChanges(coin.Name, coin.Price, _context);
				outputCoins.Add(coin);
			}

			return View(outputCoins);
		}


		[Route("Market/{coinName}")]
		public IActionResult Details(string coinName)
		{
			ViewBag.coin = coinName;

			string[] coinToArray = { coinName };
			dynamic price = Convert.ToDecimal(CryptocurrencyOperations.GetPrices(coinToArray, _context).Single().price);

			string name = coinName;
			CoinOutputModel outputCoin = new CoinOutputModel()
			{
				Name = name,
				Price = price,
			changes24h =  CryptocurrencyOperations.Get24hChanges(name, price, _context),
			DayOpenPrice = _context.OpenPrices.Where(x=> x.CoinName == name).Single().OpenPrice
			};

			return View(outputCoin);
		}

	}
}
