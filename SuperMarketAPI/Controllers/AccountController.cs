using Microsoft.AspNetCore.Mvc;

namespace SuperMarketAPI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
