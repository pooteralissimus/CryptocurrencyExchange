using ClassLibrary;
using CryptocurrencyExchange.Models;
using DbAccessLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CryptocurrencyExchange.Controllers
{
    public class AccountController : Controller
    {

        private readonly MyDbContext _context;

        public AccountController(MyDbContext context) { _context = context; }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var listOfUserCoins = _context.AccountsBalance.Where(x => x.UserId == userId && x.CoinName != "USDT").ToList();
            string[] personalCoins = new string[listOfUserCoins.Count()];
            int i = 0;
            foreach (var coin in listOfUserCoins)
            {
                personalCoins[i] = coin.CoinName;
                i++;
            }

            List<CoinOutputModel> coinsInfo = CryptocurrencyOperations.GetPrices(personalCoins, _context);
            List<AccountBalanceOutput> accountBalaceOutput = new List<AccountBalanceOutput>();
            foreach (var coin in coinsInfo)
            {
                accountBalaceOutput.Add(new AccountBalanceOutput()
                {
                    CoinName = coin.Name,
                    CoinPrice = coin.Price,
                    Quantity = listOfUserCoins.Where(x => x.CoinName == coin.Name).Single().Quantity,
                });
            }

            accountBalaceOutput = accountBalaceOutput.OrderByDescending(x => x.UsdtConvert).ToList();
            return View(accountBalaceOutput);
        }


        public IActionResult SellOrSend(string coinName, decimal coinPrice, decimal quantity, string sellOrSend)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (sellOrSend == "Sell")
                CryptocurrencyOperations.Sell(coinName, coinPrice, quantity, userId, _context);

            else if (sellOrSend == "Send")
                CryptocurrencyOperations.Send(coinName, quantity, "9893056d-0be2-4ae5-8bfc-48db005e89aa", //test userId
                    userId, _context);

            return RedirectToAction("Index", "Account");
        }

    }
}
