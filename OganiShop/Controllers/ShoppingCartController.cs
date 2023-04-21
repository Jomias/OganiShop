using Microsoft.AspNetCore.Mvc;

namespace OganiShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
