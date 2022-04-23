using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CryptocurrencyExchange.Controllers
{
    public class MarketController : Controller
    {
        public IActionResult Index()
        {
            string[] coinsName = { "BTCUSDT", "ETHUSDT", "SOLUSDT", "LUNAUSDT","BNBUSDT",
                "XRPUSDT", "DOGEUSDT", "SHIBUSDT", "AXSUSDT","MANAUSDT" };
           
            return View(CryptocurrencyOperations.GetPrices(coinsName));
        }
    }
}
