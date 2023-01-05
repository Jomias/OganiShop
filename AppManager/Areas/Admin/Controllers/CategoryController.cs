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

namespace AppManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin, staff")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _enviroment;

        public CategoryController(AppDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _enviroment = environment;
        }
        public string GetAccount()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            var account = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            return account;
        }

        public IActionResult Index(string name, int pageNumber = 1)
        {
            int pageSize = 6;
            var query = (from b1 in _dbContext.CategoryEntities
                         join b2 in _dbContext.FileManageEntities on b1.FileId equals b2.Id
                         where !b1.IsDeleted
                         where string.IsNullOrEmpty(name) || b1.Name.Trim().ToLower().Contains(name.Trim().ToLower())
                         select new CategoryModel()
                         {
                             Id = b1.Id,
                             Name = b1.Name,
                             Slug = b1.Slug,
                             FileId = b1.FileId,
                             FilePath = b2.FilePath
                         }).ToList();
            var total = query.Count();
            ViewBag.pageCount = Math.Ceiling((decimal)total / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.name = name;
            var Index = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return View(Index);
        }

        public IActionResult AddOrUpdate(int id)
        {
            if (id == 0)
            {
                ViewBag.Type = "Thêm mới";
                return View(new CategoryModel());
            }
            ViewBag.Type = "Cập nhật";
            var category = _dbContext.CategoryEntities.Find(id);
            var imagePath = _dbContext.FileManageEntities.Find(category.FileId).FilePath;
            return View(new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                FileId = category.FileId,
                FilePath = imagePath
            });
        }

        [HttpPost]
        public IActionResult AddOrUpdate(CategoryModel category)
        {
            var account = GetAccount();
            if (category.Id == 0)
            {
                _dbContext.CategoryEntities.Add(new CategoryEntity()
                {
                    Name = category.Name,
                    Slug = MakeSlug.ToUrlSlug(category.Name),
                    FileId = category.FileId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                    CreatedBy = account,
                    UpdatedBy = account,
                });
            }
            else
            {
                var entity = _dbContext.CategoryEntities.Find(category.Id);
                entity.Name = category.Name;
                entity.FileId = category.FileId;
                entity.Slug = MakeSlug.ToUrlSlug(category.Name);
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedBy = account;
                _dbContext.CategoryEntities.Update(entity);
            }
            _dbContext.SaveChanges();
            return Redirect("/Admin/Category/Index");
        }

        public IActionResult Delete(int id)
        {
            var entity = _dbContext.CategoryEntities.Find(id);
            entity.IsDeleted = true;
            _dbContext.CategoryEntities.Update(entity);
            _dbContext.SaveChanges();
            return Redirect("/Admin/Category/Index");
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            var account = GetAccount();
            if (file == null)
            {
                return Json(new { status = "error" });
            }
            string folderUploads = Path.Combine(_enviroment.WebRootPath, "img\\category");
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string fullPath = Path.Combine(folderUploads, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var fileEntity = new FileManageEntity()
            {
                Name = file.Name,
                FilePath = "/img/category/" + fileName,
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
