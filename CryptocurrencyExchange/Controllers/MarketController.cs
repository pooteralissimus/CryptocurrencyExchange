using ClassLibrary;
using CryptocurrencyExchange.Models;
using DbAccessLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CryptocurrencyExchange.Controllers
{
    public class MarketController : Controller
    {
        private readonly MyDbContext _context;
        public MarketController(MyDbContext context) { _context = context; }


        public IActionResult Index()
        {
            string[] coinsName = { "BTCUSDT", "ETHUSDT", "SOLUSDT", "LUNAUSDT","BNBUSDT",
                "XRPUSDT", "DOGEUSDT", "SHIBUSDT", "AXSUSDT","MANAUSDT" };

            List<CoinOutputModel> outputCoins = new List<CoinOutputModel>();

            var prices = CryptocurrencyOperations.GetPrices(coinsName);

            foreach (var price in prices)
            {

            }

            return View();
        }





    }
}
