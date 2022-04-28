using Microsoft.AspNetCore.Mvc;

namespace CryptocurrencyExchange.Controllers
{
    public class FuturesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
