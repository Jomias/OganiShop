using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;
using System;

namespace OganiShop.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ABlogController : ControllerBase
    {
        private readonly OganiShopContext _dbContext;
        public ABlogController(OganiShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAllBlogs")]
        public IActionResult GetAllBlogs()
        {
            try
            {
                var products = _dbContext.Blogs.Where(x => x.IsDeleted == false);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetLatestBlogs")]
        public IActionResult GetLatestBlogs()
        {
            try
            {
                var latestBlogs = _dbContext.Blogs.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate).Take(3);
                return Ok(latestBlogs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GetRelatedBlogs")]
        public IActionResult GetRelatedBlogs(int id)
        {
            try
            {
                var blog = _dbContext.Blogs.FirstOrDefault(x => x.Id == id && x.IsDeleted == false);
                var query = _dbContext.Blogs.Where(x => x.IsDeleted == false && x.Id != id && x.CategoryBlogId == blog.CategoryBlogId).Take(3).ToList();
                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
