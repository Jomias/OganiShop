using AppManager.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

namespace AppManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin, staff")]
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

        public IActionResult GetLatestMessage()
        {
            var query = _dbContext.ContactMessageEntities.Select(x => x).OrderByDescending(x => x.Time).Take(5).ToList();
            return Json(query);
        }
    }
}
