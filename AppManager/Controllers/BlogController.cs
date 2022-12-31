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

        public IActionResult Index(int categoryId, int tagId, int pageNumber = 1)
        {
            var t = (from b1 in _dbContext.BlogEntities
                     join d in _dbContext.BlogTagEntities on b1.Id equals d.BlogId
                     where (d.TagId == tagId)
                     select b1.Id).ToList();
            var query = (from b1 in _dbContext.BlogEntities
                     join b2 in _dbContext.BlogImageEntities on b1.Id equals b2.BlogId
                     join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                     join b4 in _dbContext.CategoryBlogEntities on b1.CategoryId equals b4.Id
                     where !b1.IsDeleted && b1.Status == 0 && !b2.IsDeleted && b2.IsAvatar
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
            return View(query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList());

        }

        [HttpGet]
        public IActionResult GetCategoryBlog()
        {
            var query = _dbContext.CategoryBlogEntities.Where(x => !x.IsDeleted)
                                                       .Select(x => new CategoryBlogModel()
                                                       {
                                                           Id = x.Id,
                                                           Name = x.Name,
                                                           BlogQuantity = x.BlogQuantity,
                                                       });
            return Json(query.ToList());
        }

        [HttpGet]
        public IActionResult GetAllTags()
        {
            var query = _dbContext.TagEntities.Where(x => !x.IsDeleted)
                                            .Select(x => new TagModel()
                                            {
                                                Id = x.Id,
                                                Slug = x.Slug,
                                                Name = x.Name,
                                            });
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

    }
}
