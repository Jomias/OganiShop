using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;
using OganiShop.Models;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace OganiShop.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly OganiShopContext _dbContext;
        public HomeController(OganiShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetAccount()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            if (claims.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Json("");
            }
            var account = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _dbContext.Users.Where(x => x.Account == account)
                                                .Select(x => new UserModel()
                                                {
                                                    Account = x.Account,
                                                    FirstName = x.FirstName,
                                                    LastName = x.LastName
                                                }).First();
            string username = user.FirstName + " " + user.LastName;
            return Json(username);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}