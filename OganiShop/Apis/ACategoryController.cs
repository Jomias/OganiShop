using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OganiShop.Entities;
using System;

namespace OganiShop.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ACategoryController : ControllerBase
    {
        private readonly OganiShopContext _dbContext;
        public ACategoryController(OganiShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            try
            {
                var categories = _dbContext.Categories.Where(x => x.IsDeleted == false);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
