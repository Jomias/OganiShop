using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OganiShop.Entities;
using System;

namespace OganiShop.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class AProductController : ControllerBase
    {
        private readonly OganiShopContext _dbContext;
        public AProductController(OganiShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _dbContext.Products.Where(x => x.IsDeleted == false);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetLatestProducts")]
        public IActionResult GetLatestProducts()
        {
            try
            {
                var latestProducts = _dbContext.Products.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate);
                return Ok(latestProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
