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
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

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

        public IActionResult GetFeaturedProduct()
        {

            var query = (from a1 in _dbContext.ProductImageEntities
                         join a2 in _dbContext.FileManageEntities on a1.FileId equals a2.Id
                         join b1 in _dbContext.ProductEntities on a1.ProductId equals b1.Id
                         join b2 in _dbContext.OrderDetailEntities on b1.Id equals b2.ProductId
                         where a1.IsAvatar && !b1.IsDeleted
                         group new { a1, a2, b1, b2 }
                         by new { b1.Id, b1.Name, b1.Price, a2.FilePath, b1.CategoryId, }
                         into b5
                         select new ProductModel()
                         {
                             Id = b5.Key.Id,
                             Name = b5.Key.Name,
                             Price = b5.Key.Price,
                             Avatar = b5.Key.FilePath,
                             CategoryId = b5.Key.CategoryId,
                             Quantity = b5.Sum(x => x.b2.Quantity)
                         }).OrderByDescending(x => x.Quantity)
                         .Take(10);

            var target = (from a1 in query
                          join a2 in _dbContext.CategoryEntities on a1.CategoryId equals a2.Id
                          select new
                          {
                              a1,
                              a2
                          }).ToList()
                          .GroupBy(x => new { x.a2.Id, x.a2.Name, x.a2.Slug })
                          .Select(group => new FeaturedProductModel()
                          {
                              CategoryId = group.Key.Id,
                              CategoryName = group.Key.Name,
                              CategorySlug = group.Key.Slug,
                              ListProduct = group.Select(x => x.a1).ToList()
                          }).ToList();

            return Json(target);

        }
        public IActionResult Detail(string search, int id)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return Redirect("/ShoppingGrid/Index?search=" + search);
            }
            var productDetail = (from b1 in _dbContext.ProductEntities
                                 join b2 in _dbContext.ProductImageEntities on b1.Id equals b2.ProductId
                                 join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                                 where b1.Id == id && !b1.IsDeleted && b2.IsAvatar && !b2.IsDeleted
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
                                 }).First();
            var z = productDetail.CategoryId;
            var listImages = (from b1 in _dbContext.ProductImageEntities
                              join b2 in _dbContext.FileManageEntities on b1.FileId equals b2.Id
                              where !b1.IsDeleted && b1.ProductId == id
                              select new ProductImageModel()
                              {
                                  Id = b1.Id,
                                  ProductId = id,
                                  FileId = b2.Id,
                                  FilePath = b2.FilePath,
                                  IsAvatar = b1.IsAvatar
                              }).ToList();
            var relatedProducts = (from b1 in _dbContext.ProductEntities
                                   join b2 in _dbContext.ProductImageEntities on b1.Id equals b2.ProductId
                                   join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                                   where b1.Id != id && !b1.IsDeleted && b2.IsAvatar && !b2.IsDeleted && b1.CategoryId == z
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
                                   }).Take(4).ToList();
            return View(new ProductDetailModel()
            {
                Detail = productDetail,
                ListImages = listImages,
                RelatedProducts = relatedProducts
            });
        }
    }
}
