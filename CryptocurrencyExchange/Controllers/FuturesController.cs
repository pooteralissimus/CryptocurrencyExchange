using ClassLibrary;
using DbAccessLibrary.DataAccess;
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

        public IActionResult ShortLong(string coinName, decimal coinPrice, decimal quantity, string shortOrLong)
        {

            var total = coinPrice * quantity;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usdtBalance = _context.AccountsBalance.Where(x => x.UserId == userId && x.CoinName == "USDT").SingleOrDefault();

            if(total > usdtBalance.Quantity)
                return RedirectToAction("Index", "Market");

            usdtBalance.Quantity -= total;

            return RedirectToAction("Index","Futures", new {coinName = coinName });
        }


    }
}
