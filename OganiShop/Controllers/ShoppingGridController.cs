using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;
using System;

namespace OganiShop.Controllers
{
    public class ShoppingGridController : Controller
    {
        private readonly OganiShopContext _dbContext;
        public ShoppingGridController(OganiShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(string search, int sortOrder, int id, int minPrice = 5, int maxPrice = 100, int pageNumber = 1)
        {
            var lstProduct = _dbContext.Products
                .Where(x => x.IsDeleted == false)
                .Where(x => String.IsNullOrEmpty(search) || x.Name.Trim().ToLower().Contains(search.Trim().ToLower()))
                .Include(p => p.Category)
                .Where(x => x.Category.IsDeleted == false)
                .Where(x => x.Price >= minPrice && x.Price <= maxPrice)
                .Where(x => x.CategoryId == id || id == 0);

            var discount = lstProduct.Where(x => x.Discount != 0);

            switch (sortOrder)
            {
                case 0:
                    break;
                case 1:
                    lstProduct = lstProduct.OrderBy(x => x.Name);
                    break;
                case 2:
                    lstProduct = lstProduct.OrderBy(x => x.Price);
                    break;
                case 3:
                    lstProduct = lstProduct.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            int pageSize = 6;
            int total = lstProduct.Count();
            ViewBag.pageCount = Math.Ceiling((decimal)total / pageSize);
            pageNumber = Math.Max(1, pageNumber);
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.categoryId = id;
            ViewBag.sortOrder = sortOrder;
            ViewBag.name = search;
            return View(new
            {
                ListDiscount = discount.Take(10).ToList(),
                Count = total,
                ListProduct = lstProduct
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToList(),
            });
        }
    }
}
