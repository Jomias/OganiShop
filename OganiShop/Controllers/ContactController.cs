using Microsoft.AspNetCore.Mvc;

namespace OganiShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
