using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;
using OganiShop.Models;
using System.Security.Claims;

namespace OganiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private readonly OganiShopContext _dbContext;
        private readonly IMapper _mapper;
        public HomeController(OganiShopContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IActionResult Index()
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
                                                .Select(x => new
                                                {
                                                    x.Image,
                                                    Name = x.FirstName + " " + x.LastName,
                                                }).First();
            return Json(user);
        }
    }
}
