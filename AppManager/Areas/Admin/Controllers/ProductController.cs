using AppManager.Common;
using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Xml.Linq;

namespace AppManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin, staff")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public ProductController(AppDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }
        public string GetAccount()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            var account = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            return account;
        }

        public IActionResult Index(string name, int pageNumber = 1)
        {
            int pageSize = 5;
            var query = (from b1 in _dbContext.ProductEntities
                         join b2 in _dbContext.ProductImageEntities on b1.Id equals b2.ProductId
                         join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                         join b4 in _dbContext.CategoryEntities on b1.CategoryId equals b4.Id
                         where string.IsNullOrEmpty(name) || b1.Name.Trim().ToLower().Contains(name.Trim().ToLower())
                         where !b1.IsDeleted && b2.IsAvatar && !b2.IsDeleted
                         select new ProductModel()
                         {
                             Id = b1.Id,
                             Name = b1.Name,
                             Slug = b1.Slug,
                             Price = b1.Price,
                             OldPrice = b1.OldPrice,
                             Quantity = b1.Quantity,
                             Weight = b1.Weight,
                             Unit = b1.Unit,
                             Status = (int)b1.Status,
                             CategoryName = b4.Name,
                             Avatar = b3.FilePath,
                             CreatedBy = b1.CreatedBy,
                             CreatedDate = b1.CreatedDate,
                         }).OrderBy(x => x.Id).ToList();
            var total = query.Count();
            ViewBag.pageCount = Math.Ceiling((decimal)total / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.name = name;
            var listProduct = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return View(listProduct);
        }
        public IActionResult AddOrUpdate(int id)
        {
            ViewBag.Category = _dbContext.CategoryEntities.Select(x => new CategoryModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            if (id == 0)
            {
                return View(new ProductModel());
            }

            var prd = (from b1 in _dbContext.ProductEntities
                       join b2 in _dbContext.ProductImageEntities on b1.Id equals b2.ProductId
                       join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                       where b1.Id == id && b2.IsAvatar
                       select new ProductModel()
                       {
                           Id = b1.Id,
                           Name = b1.Name,
                           Price = b1.Price,
                           OldPrice = b1.Price,
                           Description = b1.Description,
                           Summary = b1.Summary,
                           Quantity = b1.Quantity,
                           Weight = b1.Weight,
                           Unit = b1.Unit,
                           CategoryId = b1.CategoryId,
                           Status = b1.Status,
                           Avatar = b3.FilePath,
                           AvatarFileId = b3.Id,
                       }).First();
            return View(prd);
        }

        public IActionResult Delete(int id)
        {
            var entity = _dbContext.ProductEntities.Find(id);
            entity.IsDeleted = true;
            entity.UpdatedBy = GetAccount();
            entity.UpdatedDate = DateTime.Now;
            _dbContext.ProductEntities.Update(entity);
            _dbContext.SaveChanges();
            return Redirect("/Admin/Product/ListProduct");
        }
        [HttpPost]
        public IActionResult AddOrUpdate(ProductModel productModel)
        {
            var account = GetAccount();
            if (productModel.Id == 0)
            {
                if (productModel.CategoryId == 0)
                {
                    productModel.CategoryId = 1;
                }
                var prd = new ProductEntity()
                {
                    Name = productModel.Name,
                    Slug = MakeSlug.ToUrlSlug(productModel.Name),
                    Price = productModel.Price,
                    OldPrice = productModel.OldPrice,
                    Description = productModel.Description,
                    Summary = productModel.Summary,
                    Quantity = productModel.Quantity,
                    Weight = productModel.Weight,
                    Unit = productModel.Unit,
                    CategoryId = productModel.CategoryId,
                    Status = productModel.Status,
                    CreatedDate = (DateTime)productModel.CreatedDate,
                    CreatedBy = productModel.CreatedBy,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    UpdatedBy = account,
                };
                _dbContext.ProductEntities.Add(prd);
                _dbContext.SaveChanges();
                productModel.Id = prd.Id;
            }
            else
            {
                var entity = _dbContext.ProductEntities.First(x => x.Id == productModel.Id);
                entity.Name = productModel.Name;
                entity.Slug = MakeSlug.ToUrlSlug(productModel.Name);
                entity.Price = productModel.Price;
                entity.OldPrice = productModel.OldPrice;
                entity.Description = productModel.Description;
                entity.Status = productModel.Status;
                entity.Summary = productModel.Summary;
                entity.Quantity = productModel.Quantity;
                entity.Weight = productModel.Weight;
                entity.Unit = productModel.Unit;
                entity.CategoryId = productModel.CategoryId;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedBy = account;
                _dbContext.ProductEntities.Update(entity);

                var query = _dbContext.ProductImageEntities
                    .Where(x => x.ProductId == productModel.Id)
                    .FirstOrDefault(x => x.IsAvatar);
                if (query != null)
                {
                    query.IsAvatar = false;
                    query.UpdatedDate = DateTime.Now;
                    query.UpdatedBy = account;
                    _dbContext.ProductImageEntities.Update(query);
                }
            }

            _dbContext.ProductImageEntities.Add(new ProductImageEntity()
            {
                ProductId = productModel.Id,
                FileId = productModel.AvatarFileId,
                IsAvatar = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = account,
                UpdatedBy = account,
                Status = 0,
            });
            _dbContext.SaveChanges();
            return Redirect("/Admin/Product/Index");
        }

        public IActionResult ListImage(int id, int pageNumber = 1)
        {
            var query = (from b1 in _dbContext.ProductImageEntities
                         join b2 in _dbContext.FileManageEntities on b1.FileId equals b2.Id
                         where b1.ProductId == id && !b1.IsDeleted
                         select new ProductImageModel()
                         {
                             Id = b1.Id,
                             ProductId = b1.ProductId,
                             FileId = b1.FileId,
                             FilePath = b2.FilePath,
                             IsAvatar = b1.IsAvatar,
                             CreatedDate = b1.CreatedDate,
                         }).OrderByDescending(x => x.IsAvatar).ThenByDescending(x => x.CreatedDate);
            var total = query.Count();
            if (total == 0)
            {
                return Redirect("/Admin/Product/Index");
            }
            int pageSize = 6;
            ViewBag.pageCount = Math.Ceiling((decimal)total / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            var listIMG = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            ViewBag.Id = id;
            ViewBag.Count = total;
            return View(listIMG);
        }

        [HttpPost]
        public IActionResult AddImage(ProductImageModel productImage)
        {
            var account = GetAccount();
            var entity = new ProductImageEntity()
            {
                ProductId = productImage.ProductId,
                FileId = productImage.FileId,
                IsDeleted = false,
                IsAvatar = false,
                Status = 0,
                CreatedBy = account,
                CreatedDate = DateTime.Now,
                UpdatedBy = account,
                UpdatedDate = DateTime.Now,
            };
            _dbContext.ProductImageEntities.Add(entity);
            _dbContext.SaveChanges();
            return Redirect($"/Admin/Product/ListImage?id={productImage.ProductId}");
        }
        public IActionResult SetAvatar(int id)
        {
            var temp = _dbContext.ProductImageEntities.Find(id);
            var z = temp.ProductId;
            var querry = _dbContext.ProductImageEntities
                .First(x => x.ProductId == temp.ProductId && x.IsAvatar);
            temp.IsAvatar = true;
            querry.IsAvatar = false;
            _dbContext.ProductImageEntities.Update(temp);
            _dbContext.ProductImageEntities.Update(querry);
            _dbContext.SaveChanges();
            return Redirect($"/Admin/Product/ListImage?id={z}");
        }

        public IActionResult DeleteImage(int id)
        {
            var temp = _dbContext.ProductImageEntities.Find(id);
            var z = temp.ProductId;
            _dbContext.ProductImageEntities.Remove(temp);
            _dbContext.SaveChanges();
            return Redirect($"/Admin/Product/ListImage?id={z}");
        }
        public IActionResult ListDiscount(int productId, int id)
        {
            if (id == 0)
            {
                return View(new DiscountEntity()
                {
                    Id = id,
                    ProductId = productId,
                });
            }
            var query = _dbContext.DiscountEntities.First(x => x.Id == id);
            return View(query);
        }


        [HttpPost]
        public IActionResult AddOrUpdateDiscount(DiscountModel discount)
        {
            string error = "Khong the de cac khuyen mai co thoi gian trung nhau !";
            var account = GetAccount();
            if (discount.Id == 0)
            {
                var temp = _dbContext.DiscountEntities
                    .Where(x => (((x.StartDate < discount.StartDate && discount.StartDate < x.EndDate)
                    || (x.StartDate < discount.EndDate && discount.EndDate < x.EndDate)) ||
                    (discount.StartDate < x.StartDate && x.StartDate < discount.EndDate)
                    || (discount.StartDate < x.EndDate && x.EndDate < discount.EndDate))
                    && x.ProductId == discount.ProductId);
                if (temp.Any())
                {
                    TempData["Error"] = error;
                    return Redirect("/Admin/Product/ListDiscount?productId=" + discount.ProductId);
                }
                var e = new DiscountEntity()
                {
                    ProductId = discount.ProductId,
                    DiscountType = discount.DiscountType,
                    DiscountValue = discount.DiscountValue,
                    StartDate = discount.StartDate,
                    EndDate = discount.EndDate,
                    CreatedBy = account,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = account,
                    UpdatedDate = DateTime.Now,
                    Status = 0,
                };
                _dbContext.DiscountEntities.Add(e);
            }
            else
            {
                var temp = _dbContext.DiscountEntities
                    .Where(x => (((x.StartDate < discount.StartDate && discount.StartDate < x.EndDate)
                    || (x.StartDate < discount.EndDate && discount.EndDate < x.EndDate)) ||
                    (discount.StartDate < x.StartDate && x.StartDate < discount.EndDate)
                    || (discount.StartDate < x.EndDate && x.EndDate < discount.EndDate))
                    && x.ProductId == discount.ProductId && x.Id != discount.Id);
                if (temp.Any())
                {
                    TempData["Error"] = error;
                    return Redirect("/Admin/Product/ListDiscount?productId=" + discount.ProductId);
                }
                var entity = new DiscountEntity()
                {
                    Id = discount.Id,
                    ProductId = discount.ProductId,
                    DiscountType = discount.DiscountType,
                    DiscountValue = discount.DiscountValue,
                    StartDate = discount.StartDate,
                    EndDate = discount.EndDate,
                    CreatedBy = discount.CreatedBy,
                    CreatedDate = discount.CreatedDate,
                    UpdatedBy = account,
                    UpdatedDate = DateTime.Now,
                    Status = 0,
                };
                _dbContext.DiscountEntities.Update(entity);
            }
            _dbContext.SaveChanges();
            return Redirect("/Admin/Product/ListDiscount?productId=" + discount.ProductId);
        }

        public IActionResult DeleteDiscount(int id)
        {
            var z = _dbContext.DiscountEntities.Find(id);
            _dbContext.DiscountEntities.Remove(z);
            _dbContext.SaveChanges();
            return Redirect("/Admin/Product/ListDiscount?productId=" + z.ProductId);
        }
        public IActionResult GetDiscount(int productId)
        {
            var query = _dbContext.DiscountEntities.Where(x => x.ProductId == productId && !x.IsDeleted);
            return query != null ? Json(query.ToList()) : Json("");
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file == null)
            {
                return Json(new { status = "error" });
            }
            string folderUploads = Path.Combine(_environment.WebRootPath, "img\\product-image");
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string fullPath = Path.Combine(folderUploads, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var account = GetAccount();
            var fileEntity = new FileManageEntity()
            {
                Name = file.Name,
                FilePath = "/img/product-image/" + fileName,
                FileType = "image",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = account,
                UpdatedBy = account,
                Status = 0,
            };
            _dbContext.FileManageEntities.Add(fileEntity);
            var status = _dbContext.SaveChanges();
            if (status == 0)
            {
                return Json(new { status = "error" });
            }
            var model = new FileModel()
            {
                Id = fileEntity.Id,
                Name = fileEntity.Name,
                FilePath = fileEntity.FilePath
            };
            return Json(new
            {
                status = "success",
                fileInfo = model
            });
        }
    }
}
