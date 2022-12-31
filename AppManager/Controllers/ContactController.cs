using AppManager.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AppManager.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _dbContext;
        public ContactController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
