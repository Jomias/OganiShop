using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AppManager.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly AppDbContext _dbContext;
        public ProductDetailController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int id)
        {
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
