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
            ViewBag.OrderNumber = _dbContext.OrderDetails.Where(x => x.IsDeleted == false).Count();
            ViewBag.CategoryNumber = _dbContext.Categories.Where(x => x.IsDeleted == false).Count();
            ViewBag.UserNumber = _dbContext.Users.Where(x => x.IsDeleted == false).Count();
            ViewBag.ProductNumber = _dbContext.Products.Where(x => x.IsDeleted == false).Count();

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

        public IActionResult GetCategoryOrderQuantity()
        {
            var query = (from b1 in _dbContext.Products
                         join b2 in _dbContext.OrderDetails on b1.Id equals b2.ProductId
                         join b3 in _dbContext.Categories on b1.CategoryId equals b3.Id
                         where b2.IsDeleted == false
                         group new { b1, b2, b3 } by new { b3.Id, b3.Name } into b5
                         select new
                         {
                             label = b5.Key.Name,
                             value = b5.Sum(x => x.b2.Quantity)
                         }).ToList();
            return Json(query);
        }

        public IActionResult GetNumberOfOrders()
        {
            var query = _dbContext.OrderDetails
                .Where(x => x.CreatedDate != null)
                .GroupBy(x => x.CreatedDate.Value.Date)
                .Select(g => new { Date = g.Key, OrderCount = g.Count() })
                .ToList();

            return Json(query.ToList());
        }
    }
}
