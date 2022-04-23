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

            string[] coinsName = { "BTC", "ETH", "SOL", "LUNA","BNB",
                "XRP", "DOGE", "SHIB", "AXS","MANA", "LTC", "ATOM" };

            List<CoinOutputModel> outputCoins = new List<CoinOutputModel>();

            var prices = CryptocurrencyOperations.GetPrices(coinsName, _context);

            foreach (var price in prices)
            {
                string name = price.symbol;
                name = name.Remove(name.Length - 4);
                var coin = new CoinOutputModel()
                {
                    Name = name,
                    Price = Convert.ToDecimal(price.price),
                    DayOpenPrice = _context.OpenPrices.Where(x => x.CoinName == name).Single().OpenPrice,
                };

                double tmp = (double)(coin.Price - coin.DayOpenPrice); //24h changes calculate in percents
                double a = (tmp * 100) / (double)coin.Price;
                coin.changes24h = a;
                outputCoins.Add(coin);
            }

            return View(outputCoins);
        }

        public IActionResult Details()
        {
            return View();
        }

    }
}
