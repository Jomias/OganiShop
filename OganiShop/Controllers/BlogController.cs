using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;
using System;

namespace OganiShop.Controllers
{
    public class BlogController : Controller
    {
        private readonly OganiShopContext _dbContext;
        public BlogController(OganiShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index(string search, int categoryId, int tagId, int pageNumber = 1)
        {
            var t = _dbContext.BlogTags.Where(x => x.IsDeleted == false && x.Blog.IsDeleted == false && x.Tag.IsDeleted == false && (x.TagId == tagId || tagId == 0))
                .Select(x => x.Blog).Distinct();
            var query = t
                .Where(x => x.CategoryBlogId == categoryId || categoryId == 0)
                .Include(x => x.CategoryBlog)
                .Where(x => x.CategoryBlog.IsDeleted == false)
                .Where(x => String.IsNullOrEmpty(search) || x.Title.Trim().ToLower().Contains(search.Trim().ToLower()))
                .OrderByDescending(x => x.CreatedDate);
            int pageSize = 4;
            int total = query.Count();
            ViewBag.pageCount = Math.Ceiling((decimal)total / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            ViewBag.categoryId = categoryId;
            ViewBag.tagId = tagId;
            ViewBag.name = search;
            return View(query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList());
        }

        public IActionResult Detail(string search, int id)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return Redirect("/Blog/Index?search=" + search);
            }


            var query = _dbContext.Blogs
                .Include(x => x.CategoryBlog)
                .FirstOrDefault(x => x.Id == id && x.IsDeleted == false && x.CategoryBlog.IsDeleted == false);

            if (query == null)
            {
                return BadRequest();
            }

            var temp = new
            {
                query.Id,
                query.Title,
                AuthorName = query.CreatedBy,
                AuthorRole = "admin",
                query.Content,
                Category = query.CategoryBlog.Name,
                Avatar = query.Image,
                query.CreatedDate,
                ListTags = _dbContext.BlogTags.Where(x => x.Blog.Id == id && x.IsDeleted == false).Select(x => x.Tag).Distinct().Where(x => x.IsDeleted == false)
            };
            return View(temp);
        }
    }
}
