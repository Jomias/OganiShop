using Microsoft.AspNetCore.Mvc;

namespace OganiShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
