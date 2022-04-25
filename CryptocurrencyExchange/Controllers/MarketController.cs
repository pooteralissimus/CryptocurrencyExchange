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
        private readonly string[] coinsName = { "BTC","ETH", "XRP", "DOGE", "LUNA",
                "SOL", "ATOM", "AXS", "MANA"};
        public MarketController(MyDbContext context) { _context = context; }


        public IActionResult Index()
        {
            List<CoinOutputModel> outputCoins = new List<CoinOutputModel>();

            var coins = CryptocurrencyOperations.GetPrices(coinsName, _context);

            return View(coins);
        }


        [Route("Market/{coinName}")]
        public IActionResult Details(string coinName)
        {
            string[] coinToArray = { coinName };
            var outputCoin = CryptocurrencyOperations.GetPrices(coinToArray, _context).Single();

            ViewBag.coin = outputCoin;

            //trending cryptos part
            string[] trengingCoins = { "BTC", "ETH", "LUNA" };
            var trendingCryptos = CryptocurrencyOperations.GetPrices(trengingCoins, _context);
            return View(trendingCryptos);
        }

    }
}
