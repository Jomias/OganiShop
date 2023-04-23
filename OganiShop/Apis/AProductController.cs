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
        [HttpGet("GetFeaturedProduct")]
        public IActionResult GetFeaturedProduct()
        {

            var query = (from b1 in _dbContext.Products
                         join b2 in _dbContext.OrderDetails on b1.Id equals b2.ProductId
                         where b2.IsDeleted == false
                         group new { b1, b2 }
                         by new { b1.Id, b1.Name, b1.Price, b1.Discount, b1.Image, b1.CategoryId, }
                         into b5
                         select new
                         {
                             b5.Key.Id,
                             b5.Key.Name,
                             Price = b5.Key.Price - b5.Key.Discount,
                             Avatar = b5.Key.Image,
                             b5.Key.CategoryId,
                             Quantity = b5.Sum(x => x.b2.Quantity)
                         }).OrderByDescending(x => x.Quantity)
                         .Take(10);

            var target = (from a1 in query
                          join a2 in _dbContext.Categories on a1.CategoryId equals a2.Id
                          select new
                          {
                              a1,
                              a2
                          }).ToList()
                          .GroupBy(x => new { x.a2.Id, x.a2.Name, x.a2.Slug })
                          .Select(group => new
                          {
                              CategoryId = group.Key.Id,
                              CategoryName = group.Key.Name,
                              CategorySlug = group.Key.Slug,
                              ListProduct = group.Select(x => x.a1).ToList()
                          }).ToList();

            return Ok(target);

        }
    }
}
