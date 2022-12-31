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

        public IActionResult Index()
        {
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

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _dbContext.CategoryEntities.ToList();
            return Json(categories);
        }

        [HttpGet]
        public IActionResult GetCategoryCarousel()
        {
            var ctImg = (from b1 in _dbContext.CategoryEntities
                         join b2 in _dbContext.FileManageEntities
                         on b1.FileId equals b2.Id
                         select new CategoryCarouselModel()
                         {
                             Id = b1.FileId,
                             Name = b1.Name,
                             Slug = b1.Slug,
                             FileId = b1.FileId,
                             FilePath = b2.FilePath,
                         }).ToList();
            return Json(ctImg);
        }

        [HttpGet]
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

        [HttpGet]
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

        [HttpGet]
        public IActionResult GetLatestBlogs()
        {
            var query = (from b1 in _dbContext.BlogEntities
                         join b2 in _dbContext.BlogImageEntities on b1.Id equals b2.BlogId
                         join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                         where !b1.IsDeleted && b1.Status == 0 && !b2.IsDeleted && b2.IsAvatar
                         orderby b1.CreatedDate descending
                         select new BlogModel()
                         {
                             Id = b1.Id,
                             Title = b1.Title,
                             Description = b1.Description,
                             Content = b1.Content,
                             BlogAvatarId = b3.Id,
                             BlogAvatarFilePath = b3.FilePath,
                             CreatedDate = b1.CreatedDate,
                             UpdatedDate = b1.UpdatedDate,
                             CreatedBy = b1.CreatedBy,
                             UpdatedBy = b1.UpdatedBy
                         });

            return Json(query.ToList().Take(3));
        }

        [HttpGet]
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
