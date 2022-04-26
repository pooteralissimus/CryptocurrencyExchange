using ClassLibrary;
using CryptocurrencyExchange.Models;
using DbAccessLibrary.DataAccess;
using DbAccessLibrary.Models;
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
            var list = _context.AccountsBalance.Where(x => x.UserId == userId && x.CoinName != "USDT").ToList();
            string[] personalCoins = new string[list.Count()];
            int i = 0;
            foreach (var coin in list)
            {
                personalCoins[i] = coin.CoinName;
                i++;
            }

            List<CoinOutputModel> coinsInfo = CryptocurrencyOperations.GetPrices(personalCoins,_context);
            List<AccountBalanceOutput> accountBalaceOutput = new List<AccountBalanceOutput>();
            foreach(var coin in coinsInfo)
            {
                accountBalaceOutput.Add(new AccountBalanceOutput()
                {
                    CoinName = coin.Name,
                    CoinPrice = coin.Price,
                    Quantity = list.Where(x => x.CoinName == coin.Name).Single().Quantity
                });
            }
            

            return View(accountBalaceOutput);
        }


        public IActionResult Sell(string coinName, decimal coinPrice, decimal quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usdtQuantity = coinPrice * quantity;
            var accountBalance = _context.AccountsBalance.Where(x => x.UserId == userId).ToList();
            accountBalance.Where(x => x.CoinName == "USDT").Single().Quantity += usdtQuantity;
            accountBalance.Where(x => x.CoinName == coinName).Single().Quantity -= quantity;
            _context.SaveChanges();
            return RedirectToAction("Index", "Account");
        }
    }
}
