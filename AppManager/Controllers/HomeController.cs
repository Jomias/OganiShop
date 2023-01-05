using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return Redirect("/ShoppingGrid/Index?search=" + search);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult GetBanner()
        {
            var banner = (from b1 in _dbContext.BannerEntities
                          join b2 in _dbContext.FileManageEntities on b1.FileId equals b2.Id
                          orderby b1.Type
                          select new BannerModel()
                          {
                              Id = b1.Id,
                              ImagePath = b2.FilePath,
                              Type = b1.Type
                          });
            return Json(banner);
        }

        public IActionResult GetAccount()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            if (claims.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Json("");
            }
            var account = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _dbContext.UserEntities.Where(x => x.Account == account)
                                                .Select(x => new UserModel()
                                                {
                                                    Account = x.Account,
                                                    FirstName = x.FirstName,
                                                    LastName = x.LastName
                                                }).First();
            string username = user.FirstName + " " + user.LastName;
            return Json(username);
        }
    }
}
