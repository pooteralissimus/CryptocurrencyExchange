using ClassLibrary;
using ClassLibrary.Models;
using CryptocurrencyExchange.Models;
using DbAccessLibrary.DataAccess;
using DbAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace CryptocurrencyExchange.Controllers
{
    public class FuturesController : Controller
    {
        private readonly MyDbContext _context;
        public FuturesController(MyDbContext context) { _context = context; }


        public IActionResult Index(string coinName)
        {
            string[] coinToArray = { coinName };
            var outputCoin = CryptocurrencyOperations.GetPrices(coinToArray, _context).Single();

            ViewBag.coin = outputCoin;

            //trending cryptos part
            string[] trengingCoins = { "BTC", "ETH", "LUNA" };
            var trendingCryptos = CryptocurrencyOperations.GetPrices(trengingCoins, _context);
            return View(trendingCryptos);
        }

        public IActionResult ShortLong(string coinName, decimal coinPrice, decimal quantity, string shortOrLong, int leverage = 10)
        {
            var total = coinPrice * quantity;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usdtBalance = _context.AccountsBalance.Where(x => x.UserId == userId && x.CoinName == "USDT").SingleOrDefault();

            if (total > usdtBalance.Quantity)
                return RedirectToAction("Index", "Market");

            usdtBalance.Quantity -= total;
            FuturesData position = new FuturesData()
            {
                UserId = userId,
                CoinName = coinName,
                OpenPrice = coinPrice,
                Usdt = total,
                LongShort = shortOrLong,
                Leverage = leverage
            };
            _context.FuturesDatas.Add(position);
            _context.SaveChanges();
            return RedirectToAction("Index", "Futures", new { coinName = coinName });
        }


        [Route("Futures/Position/{coinName}")]
        public IActionResult Position(string coinName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            FuturesData position = _context.FuturesDatas.Where(x => x.UserId == userId && x.CoinName == coinName).Single();
            var output = CryptocurrencyOperations.LongShort(position,_context);

            return View(output);
        }

        public IActionResult ClosePosition(string coinName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var position = _context.FuturesDatas.Where(x=>x.UserId == userId && x.CoinName == coinName).Single();
            _context.FuturesDatas.Remove(position);
            var usdtBalance = _context.AccountsBalance.Where(x => x.UserId == userId && x.CoinName == "USDT").Single();
            usdtBalance.Quantity = CryptocurrencyOperations.LongShort(position, _context).CurrentTotal;
            _context.SaveChanges();
            return RedirectToAction("Details", "Market", new {coinName = coinName});
        }


    }
}
