using Microsoft.AspNetCore.Mvc;

namespace OganiShop.Controllers
{
    public class ShoppingGridController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
