using Microsoft.AspNetCore.Mvc;

namespace OganiShop.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
