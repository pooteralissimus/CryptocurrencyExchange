using ClassLibrary;
using CryptocurrencyExchange.Models;
using DbAccessLibrary.DataAccess;
using DbAccessLibrary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CryptocurrencyExchange.Controllers
{
    public class MarketController : Controller
    {
        private readonly MyDbContext _context;
        private readonly string[] _coinsList = { "BTC","ETH", "XRP", "DOGE", "LUNA",
                "SOL", "ATOM", "AXS", "MANA"};
        public static List<CoinOutputModel> _coins;
        public MarketController(MyDbContext context) { _context = context; }


        public IActionResult Index(List<CoinOutputModel> coins = null)
        {
            if(coins.Count() == 0)
                _coins = CryptocurrencyOperations.GetPrices(_coinsList, _context);

            return View(_coins);
        }

        [HttpGet]
        public IActionResult Sort(List<CoinOutputModel> coins)
        {
            _coins = _coins.OrderByDescending(x => x.Changes24h).ToList();
            return View("Index", _coins);
        }
         

        [Route("Market/info/{coinName}")]
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

        [HttpPost]
        public IActionResult Buy(string coinName, decimal coinPrice, decimal quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var balance = _context.AccountsBalance.Where(x => x.UserId == userId && x.CoinName == "USDT").SingleOrDefault();
            decimal total = coinPrice * quantity;

            if (total > balance.Quantity)
                return RedirectToAction("Index", "Market");

            balance.Quantity -= total;
            _context.AccountsBalance.Add(new AccountBalance()
            {
                UserId = userId,
                CoinName = coinName,
                Quantity = quantity
            });
            _context.SaveChanges();
            return RedirectToAction("Index", "Market");
        }


    }
}
