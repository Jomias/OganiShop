using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppManager.Controllers
{
    public class ShoppingGridController : Controller
    {
        private readonly AppDbContext _dbContext;
        public ShoppingGridController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int id = 0, int minPrice = 5, int maxPrice = 100, int pageNumber = 1)
        {
            var discount = (from b1 in _dbContext.ProductEntities
                            join b2 in _dbContext.DiscountEntities on b1.Id equals b2.ProductId
                            join b3 in _dbContext.ProductImageEntities on b1.Id equals b3.ProductId
                            join b4 in _dbContext.FileManageEntities on b3.FileId equals b4.Id
                            join b5 in _dbContext.CategoryEntities on b1.CategoryId equals b5.Id
                            where !b1.IsDeleted && b3.IsAvatar && !b3.IsDeleted
                            where b2.CreatedDate <= DateTime.Now && b2.EndDate >= DateTime.Now
                            where b5.Id == id || id == 0
                            select new ProductDiscountModel()
                            {
                                Id = b1.Id,
                                Name = b1.Name,
                                Slug = b1.Slug,
                                Price = b2.DiscountType == 0 ? (b1.Price - b2.DiscountValue) : (b1.Price * (1 - b2.DiscountValue / 100)),
                                OldPrice = b1.Price,
                                Category = b5.Name,
                                DiscountType = b2.DiscountType,
                                DiscountValue = b2.DiscountValue,
                                Avatar = b4.FilePath,
                                AvatarFileId = b4.Id
                            }).Where(x => x.Price >= minPrice && x.Price <= maxPrice);
            var prd = (from b1 in _dbContext.ProductImageEntities
                       join b2 in _dbContext.FileManageEntities on b1.FileId equals b2.Id
                       join b3 in _dbContext.ProductEntities on b1.ProductId equals b3.Id
                       join b4 in _dbContext.CategoryEntities on b3.CategoryId equals b4.Id
                       where !b3.IsDeleted && (b4.Id == id || id == 0)
                       where b1.IsAvatar && !b1.IsDeleted
                       select new ProductModel()
                       {
                           Id = b3.Id,
                           Name = b3.Name,
                           Slug = b3.Slug,
                           Price = b3.Price,
                           OldPrice = b3.OldPrice,
                           Description = b3.Description,
                           Summary = b3.Summary,
                           Quantity = b3.Quantity,
                           Weight = b3.Weight,
                           Unit = b3.Unit,
                           CategoryId = b3.CategoryId,
                           Avatar = b2.FilePath,
                           AvatarFileId = b2.Id
                       }).Where(x => x.Price >= minPrice && x.Price <= maxPrice);
            int pageSize = 6;
            int total = prd.Count();
            ViewBag.pageCount = Math.Ceiling((decimal)total / pageSize);
            pageNumber = Math.Max(1, pageNumber);
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.categoryId = id;
            var data = new ShoppingGridModel()
            {
                ListDiscount = discount.Take(10).ToList(),
                Count = prd.Count(),
                ListProduct = prd.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList()
            };
            return View(data);
        }

        [HttpGet]
        public IActionResult GetLatestProducts()
        {
            var latestProducts = (from b1 in _dbContext.ProductEntities
                                  join b2 in _dbContext.ProductImageEntities on b1.Id equals b2.ProductId
                                  join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                                  where !b1.IsDeleted && b2.IsAvatar && !b2.IsDeleted
                                  orderby b1.CreatedDate descending
                                  select new ProductModel()
                                  {
                                      Id = b1.Id,
                                      Name = b1.Name,
                                      Slug = b1.Slug,
                                      Price = b1.Price,
                                      OldPrice = b1.OldPrice,
                                      Description = b1.Description,
                                      Summary = b1.Summary,
                                      Quantity = b1.Quantity,
                                      Weight = b1.Weight,
                                      Unit = b1.Unit,
                                      CategoryId = b1.CategoryId,
                                      Avatar = b3.FilePath,
                                      AvatarFileId = b3.Id
                                  }).Take(9).ToList();
            return Json(latestProducts);
        }
    }
}
