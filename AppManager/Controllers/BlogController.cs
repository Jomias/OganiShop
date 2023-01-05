using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AppManager.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;
        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index(string search, int categoryId, int tagId, int pageNumber = 1)
        {
            var name = String.IsNullOrEmpty(search) ? "" : search;
            var t = (from b1 in _dbContext.BlogEntities
                     join d in _dbContext.BlogTagEntities on b1.Id equals d.BlogId
                     where (d.TagId == tagId)
                     select b1.Id).ToList();
            var query = (from b1 in _dbContext.BlogEntities
                     join b2 in _dbContext.BlogImageEntities on b1.Id equals b2.BlogId
                     join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                     join b4 in _dbContext.CategoryBlogEntities on b1.CategoryId equals b4.Id
                     where !b1.IsDeleted && b1.Status == 0 && !b2.IsDeleted && b2.IsAvatar
                     where b1.Title.Trim().ToLower().Contains(name.Trim().ToLower())
                     where (b4.Id == categoryId || categoryId == 0)
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
                     }).Where(x => t.Contains(x.Id) || tagId == 0);
            int pageSize = 4;
            int total = query.Count();
            ViewBag.pageCount = Math.Ceiling((decimal)total / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.categoryId = categoryId;
            ViewBag.tagId = tagId;
            ViewBag.name = name;
            return View(query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList());
        }

        public IActionResult GetCategoryBlog()
        {
            var temp = (from b1 in _dbContext.CategoryBlogEntities
                        join b2 in _dbContext.BlogEntities on b1.Id equals b2.CategoryId
                        where !b2.IsDeleted && !b1.IsDeleted && b1.Status == 0
                        group new { b1, b2 } by new { b1.Id, b1.Name, b1.Slug } into b3
                        select new CategoryBlogModel()
                        {
                            Id = b3.Key.Id,
                            Name = b3.Key.Name,
                            Slug = b3.Key.Slug,
                            BlogQuantity = b3.Count()
                        });
            return Json(temp.ToList());
        }

        public IActionResult GetAllTags()
        {
            var query = _dbContext.TagEntities.Where(x => !x.IsDeleted);    
            return Json(query.ToList());
        }

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
        public IActionResult Detail(string search, int id)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return Redirect("/Blog/Index?search=" + search);
            }
            var query = (from b1 in _dbContext.BlogEntities
                         join b2 in _dbContext.CategoryBlogEntities on b1.CategoryId equals b2.Id
                         join b3 in _dbContext.AccountManagerEntities on b1.CreatedBy equals b3.Account
                         join b4 in _dbContext.AccountImageEntities on b3.Account equals b4.Account
                         join b5 in _dbContext.FileManageEntities on b4.FileId equals b5.Id
                         join b6 in _dbContext.UserEntities on b3.Account equals b6.Account
                         where !b1.IsDeleted && b1.Id == id && b4.IsAvatar && b1.Status == 0
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
                             Avatar = b5.FilePath
                         }).First();
            query.ListTags = (from b1 in _dbContext.BlogTagEntities
                              join b2 in _dbContext.TagEntities on b1.TagId equals b2.Id
                              where b1.BlogId == id
                              select b2.Name).ToList();
            return View(query);
        }

        public IActionResult GetRelatedBlogs(int id)
        {
            var query = (from b1 in _dbContext.BlogEntities
                         join b2 in _dbContext.BlogImageEntities on b1.Id equals b2.BlogId
                         join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                         where !b1.IsDeleted && b1.Status == 0 && !b2.IsDeleted && b2.IsAvatar && b1.Id != id
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
                         }).ToList().Take(3);
            return Json(query);
        }
    }
}
