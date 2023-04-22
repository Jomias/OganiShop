using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OganiShop.Entities;
using System;

namespace OganiShop.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ACategoryBlogController : ControllerBase
    {
        private readonly OganiShopContext _dbContext;
        public ACategoryBlogController(OganiShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAllCategoryBlogs")]
        public IActionResult GetAllCategoryBlogs()
        {
            try
            {
                var products = _dbContext.CategoryBlogs.Where(x => x.IsDeleted == false)
                    .Select(x => new {
                        x.Id,
                        x.Name,
                        x.Slug,
                        Quantity = x.Blogs.Where(x => x.IsDeleted == false).Count()
                    });;
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
