using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace AppManager.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly AppDbContext _dbContext;
        public BlogDetailController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int id)
        {
            var query = (from b1 in _dbContext.BlogEntities
                         join b2 in _dbContext.CategoryBlogEntities on b1.CategoryId equals b2.Id
                         join b3 in _dbContext.AccountManagerEntities on b1.CreatedBy equals b3.Account
                         join b4 in _dbContext.AccountImageEntities on b3.Account equals b4.Account
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
                             Avatar = b5.FilePath
                         }).First();
            query.ListTags = (from b1 in _dbContext.BlogTagEntities
                              join b2 in _dbContext.TagEntities on b1.TagId equals b2.Id
                              where b1.BlogId == id
                              select b2.Name).ToList();
            return View(query);
        }

        [HttpGet]
        public IActionResult GetRelatedBlogs(int id)
        {
            var query = (from b1 in _dbContext.BlogEntities
                         join b2 in _dbContext.BlogImageEntities on b1.Id equals b2.BlogId
                         join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                         where !b1.IsDeleted  && b1.Status == 0 && !b2.IsDeleted && b2.IsAvatar && b1.Id != id
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
