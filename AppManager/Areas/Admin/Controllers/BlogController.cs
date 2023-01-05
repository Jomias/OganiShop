using Microsoft.AspNetCore.Mvc;

namespace AppManager.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        [HttpGet]
        public IActionResult Blog()
        {
            return View();
        }
    }
}
