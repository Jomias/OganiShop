using Microsoft.AspNetCore.Mvc;
using OganiShop.Entities;
using System;

namespace OganiShop.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly OganiShopContext _dbContext;
        public CheckOutController(OganiShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
