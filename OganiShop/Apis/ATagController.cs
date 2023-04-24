using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OganiShop.Entities;
using System;

namespace OganiShop.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATagController : ControllerBase
    {
        private readonly OganiShopContext _dbContext;
        public ATagController(OganiShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAllTags")]
        public IActionResult GetAllTags()
        {
            try
            {
                var products = _dbContext.Tags.Where(x => x.IsDeleted == false);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllTagByBlogId")]
        public IActionResult GetAllTagByBlogId(int BlogId)
        {
            try
            {
                var tags = _dbContext.Tags
                    .Where(x => x.IsDeleted == false)
                    .Where(tag => !tag.BlogTags.Any(blogTag => blogTag.BlogId == BlogId && blogTag.IsDeleted == false)).ToList();
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
