using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppManager.Common;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

namespace AppManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin, staff, collaborator")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;
        public BlogController(AppDbContext dbContext, IWebHostEnvironment environment)
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

        public IActionResult GetAuthorInfo()
        {
            var account = GetAccount();
            var query = (from b1 in _dbContext.AccountManagerEntities
                         join b2 in _dbContext.AccountImageEntities on b1.Account equals b2.Account
                         join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                         join b4 in _dbContext.UserEntities on b1.Account equals b4.Account
                         where b1.Account == account
                         select new BlogDetailModel()
                         {
                             AuthorName = b4.FirstName + " " + b4.LastName,
                             AuthorRole = b1.Role,
                             Avatar = b3.FilePath,
                         }).First();
            return Json(query);
        }
        public IActionResult AddOrUpdate(int id)
        {


            var query = (from b1 in _dbContext.BlogEntities
                         join b2 in _dbContext.CategoryBlogEntities on b1.CategoryId equals b2.Id
                         join b3 in _dbContext.AccountManagerEntities on b1.CreatedBy equals b3.Account
                         join b4 in _dbContext.BlogImageEntities on b1.Id equals b4.BlogId
                         join b5 in _dbContext.FileManageEntities on b4.FileId equals b5.Id
                         join b6 in _dbContext.UserEntities on b3.Account equals b6.Account
                         where !b1.IsDeleted && (b1.Id == id || id == 0) && b4.IsAvatar
                         select new BlogDetailModel()
                         {
                             Id = b1.Id,
                             Title = b1.Title,
                             AuthorName = b6.FirstName + " " + b6.LastName,
                             CreatedDate = b1.CreatedDate,
                             Description = b1.Description,
                             Content = Regex.Replace(b1.Content, @"\t|\n|\r", ""),
                             AuthorRole = b3.Role,
                             Category = b2.Name,
                             Avatar = b5.FilePath,
                             CategoryId = b2.Id,
                             AvatarId = b5.Id
                         }).First();
            query.ListTags = (from b1 in _dbContext.BlogTagEntities
                              join b2 in _dbContext.TagEntities on b1.TagId equals b2.Id
                              where b1.BlogId == id || id == 0
                              select b2.Name).ToList();
            return id == 0 ? View() : View(query);
        }

        [HttpPost]
        public IActionResult AddOrUpdate(BlogDetailModel model)
        {
            if (model.Id == 0)
            {
                TempData["Alert"] = "Đã thêm bài viết mới thành công!";
            }
            else
            {
                TempData["Alert"] = "Đã sửa bài viết thành công!";
            }

            var account = GetAccount();

            var entity = _dbContext.BlogEntities.Find(model.Id);
            if (entity == null)
            {
                entity = new BlogEntity()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    CategoryId = model.CategoryId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = account,
                    UpdatedBy = account,
                    Status = model.Status,
                };
                _dbContext.BlogEntities.Add(entity);

            }
            else
            {
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.Content = model.Content;
                entity.CategoryId = model.CategoryId;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedBy = account;
                entity.Status = model.Status;
                _dbContext.BlogEntities.Update(entity);
            }
            _dbContext.SaveChanges();
            model.Id = entity.Id;
            var img = _dbContext.BlogImageEntities.Where(x => x.BlogId == model.Id && x.IsAvatar == true);
            if (img.Any())
            {
                var avatar = img.First();
                avatar.IsAvatar = false;
                avatar.UpdatedDate = DateTime.Now;
                avatar.UpdatedBy = account;
                _dbContext.BlogImageEntities.Update(avatar);
            }
            _dbContext.BlogImageEntities.Add(new BlogImageEntity()
            {
                BlogId = model.Id,
                FileId = model.AvatarId,
                IsAvatar = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = account,
                Status = 0,
                UpdatedBy = account,
            });
            _dbContext.SaveChanges();

            var tags = model.ListTags[0].ToString().Split(",").ToList();
            foreach (var tag in tags)
            {
                if (!_dbContext.TagEntities.Where(x => x.Name == tag).Any())
                {
                    var e = new TagEntity()
                    {
                        Name = tag,
                        Slug = tag.Replace(" ", "-").ToLower(),
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CreatedBy = account,
                        UpdatedBy = account,
                        Status = 0,
                    };
                    _dbContext.TagEntities.Add(e);
                    _dbContext.SaveChanges();
                    _dbContext.BlogTagEntities.Add(new BlogTagEntity()
                    {
                        BlogId = model.Id,
                        TagId = e.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CreatedBy = account,
                        UpdatedBy = account,
                        Status = 0,
                    });
                    _dbContext.SaveChanges();
                }
            }
            var q = (from a in _dbContext.BlogTagEntities
                     join b in _dbContext.TagEntities on a.TagId equals b.Id
                     where a.BlogId == model.Id && !a.IsDeleted
                     select new TagModel()
                     {
                         Id = b.Id,
                         Slug = b.Slug,
                         Name = b.Name,
                     }).ToList();
            foreach (var item in q)
            {
                if (!tags.Contains(item.Name))
                {
                    var pt = _dbContext.BlogTagEntities.First(x => x.TagId == item.Id && x.BlogId == model.Id);
                    pt.IsDeleted = true;
                    pt.UpdatedBy = account;
                    pt.UpdatedDate = DateTime.Now;
                    _dbContext.BlogTagEntities.Update(pt);
                    _dbContext.SaveChanges();
                }
            }

            foreach (var tag in tags)
            {
                var id = _dbContext.TagEntities.First(x => x.Name == tag).Id;
                var pt = _dbContext.BlogTagEntities.Where(x => x.TagId == id && x.BlogId == model.Id && !x.IsDeleted);
                if (!pt.Any())
                {
                    var e = new BlogTagEntity()
                    {
                        BlogId = model.Id,
                        TagId = id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        CreatedBy = account,
                        UpdatedBy = account,
                    };
                    _dbContext.BlogTagEntities.Add(e);
                }
            }
            _dbContext.SaveChanges();
            return Redirect("/Admin/Blog/AddOrUpdate?id=" + entity.Id);
        }

        public IActionResult Index(int id, string name, int pageNumber = 1)
        {
            var account = GetAccount();
            ViewBag.account = account;
            ViewBag.role = _dbContext.AccountManagerEntities.First(x => x.Account == account).Role;
            var query = (from b1 in _dbContext.BlogEntities
                         join b2 in _dbContext.CategoryBlogEntities on b1.CategoryId equals b2.Id
                         join b3 in _dbContext.BlogImageEntities on b1.Id equals b3.BlogId
                         join b4 in _dbContext.AccountManagerEntities on b1.CreatedBy equals b4.Account
                         join b5 in _dbContext.FileManageEntities on b3.FileId equals b5.Id
                         join b6 in _dbContext.UserEntities on b4.Account equals b6.Account
                         where string.IsNullOrEmpty(name) || b1.Title.Trim().ToLower().Contains(name.Trim().ToLower())
                         where !b1.IsDeleted && (b1.Status == 0 || (b1.Status != 0 && b4.Account == account) || b4.Role == "admin")
                         where !b3.IsDeleted && b3.IsAvatar
                         where (b2.Id == id || id == 0) 
                         select new BlogDetailModel()
                         {
                             Id = b1.Id,
                             Title = b1.Title,
                             AuthorName = b6.FirstName + " " + b6.LastName,
                             CreatedDate = b1.CreatedDate,
                             Description = b1.Description,
                             Content = b1.Content,
                             AuthorRole = b4.Role,
                             CategoryId = b2.Id,
                             Category = b2.Name,
                             Avatar = b5.FilePath,
                             Account = b4.Account,
                         });
            int pageSize = 5;
            var total = query.Count();
            ViewBag.pageCount = Math.Ceiling((decimal)total / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.name = name;
            ViewBag.Id = id;
            return View(query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList());
        }

        public IActionResult AddCategoryBlog(string name)
        {
            TempData["Alert"] = "Add SuccessFully";
            _dbContext.CategoryBlogEntities.Add(new CategoryBlogEntity()
            {
                Name = name,
                Slug = MakeSlug.ToUrlSlug(name),
                CreatedDate = DateTime.Now,
                CreatedBy = GetAccount(),
                UpdatedDate = DateTime.Now,
                UpdatedBy = GetAccount(),
                Status = 0
            });
            _dbContext.SaveChanges();
            return Redirect("/Admin/Blog/Index");
        }
        public IActionResult DeleteCategoryBlog(int id)
        {
            var query = (from b1 in _dbContext.CategoryBlogEntities
                         join b2 in _dbContext.BlogEntities on b1.Id equals b2.CategoryId
                         where b1.Id == id && !b2.IsDeleted
                         select new
                         {
                             b1.Id
                         });
            if (query.Count() > 0) 
            {
                TempData["Alert"] = "Cannot Delete This Category";
                return Redirect("/Admin/Blog/Index");
            }
            var z = _dbContext.CategoryBlogEntities.Find(id);
            _dbContext.CategoryBlogEntities.Remove(z);
            _dbContext.SaveChanges();
            return Redirect("/Admin/Blog/Index");
        }

        public IActionResult Detail(int id)
        {
            ViewBag.account = GetAccount();
            ViewBag.role = _dbContext.AccountManagerEntities.First(x => x.Account == GetAccount()).Role;
            var query = (from b1 in _dbContext.BlogEntities
                         join b2 in _dbContext.CategoryBlogEntities on b1.CategoryId equals b2.Id
                         join b3 in _dbContext.AccountManagerEntities on b1.CreatedBy equals b3.Account
                         join b4 in _dbContext.BlogImageEntities on b1.Id equals b4.BlogId
                         join b5 in _dbContext.FileManageEntities on b4.FileId equals b5.Id
                         join b6 in _dbContext.UserEntities on b3.Account equals b6.Account
                         where !b1.IsDeleted && b1.Id == id && b4.IsAvatar
                         select new BlogDetailModel()
                         {
                             Id = b1.Id,
                             Title = b1.Title,
                             AuthorName = b6.FirstName + " " + b6.LastName,
                             CreatedDate = b1.CreatedDate,
                             Description = b1.Description,
                             Content = b1.Content,
                             AuthorRole = b3.Role,
                             Category = b2.Name,
                             Avatar = b5.FilePath,
                             CategoryId = b2.Id,
                             Account = b3.Account,
                         }).First();
            query.ListTags = (from b1 in _dbContext.BlogTagEntities
                              join b2 in _dbContext.TagEntities on b1.TagId equals b2.Id
                              where b1.BlogId == id
                              select b2.Name).ToList();
            return View(query);
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file == null)
            {
                return Json(new { status = "error" });
            }
            string folderUploads = Path.Combine(_environment.WebRootPath, "img\\blog\\avatar");
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string fullPath = Path.Combine(folderUploads, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var fileEntity = new FileManageEntity()
            {
                Name = file.Name,
                FilePath = "/img/blog/avatar/" + fileName,
                FileType = "image",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = GetAccount(),
                Status = 0,
                UpdatedBy = GetAccount(),
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
