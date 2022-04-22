using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CryptocurrencyExchange.Controllers
{
    public class MarketController : Controller
    {
        public IActionResult Index()
        {
            List<dynamic> coinsPrice = new List<dynamic>();
            string[] coinsName = { "BTCUSDT", "ETHUSDT", "SOLUSDT", "LUNAUSDT","BNBUSDT",
                "XRPUSDT", "DOGEUSDT", "SHIBUSDT", "AXSUSDT","MANAUSDT" };
            string json;

            foreach(var coinName in coinsName)
            {
                using (var web = new System.Net.WebClient())
                {
                    var url = $"https://api.binance.com/api/v3/ticker/price?symbol={coinName}";
                    json = web.DownloadString(url);
                }

                dynamic coinInfo = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                coinsPrice.Add(coinInfo);
            }
          
            return View(coinsPrice);
        }
    }
}
