using AppManager.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Linq;
using AppManager.Models;

namespace AppManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAccount()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            var account = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = (from b1 in _dbContext.AccountManagerEntities
                        join b2 in _dbContext.UserEntities on b1.Account equals b2.Account
                        join b3 in _dbContext.AccountImageEntities on b1.Account equals b3.Account
                        join b4 in _dbContext.FileManageEntities on b3.FileId equals b4.Id
                        where b1.Account == account && !b3.IsDeleted && b3.IsAvatar
                        select new UserModel()
                        {
                            Account = account,
                            FirstName = b2.FirstName,
                            LastName = b2.LastName,
                            Role = b1.Role,
                            AvatarId = b4.Id,
                            AvatarPath = b4.FilePath,
                        }).First();
            return Json(user);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
