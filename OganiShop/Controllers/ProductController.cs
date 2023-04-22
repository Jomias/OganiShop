using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;

namespace OganiShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly OganiShopContext _dbContext;
        public ProductController(OganiShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(string search, int id)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return Redirect("/ShoppingGrid/Index?search=" + search);
            }

            var productDetail = _dbContext.Products
                .Include(x => x.ProductImages)
                .FirstOrDefault(x => x.Id == id && x.IsDeleted == false);

            if (productDetail == null)
            {
                return BadRequest();
            }
            var listImages = productDetail.ProductImages.ToList();
            var relatedProducts = _dbContext.Products
                .Where(x => x.IsDeleted == false && x.Id != id && x.CategoryId == productDetail.CategoryId)
                .ToList();
            return View(new
            {
                Detail = productDetail,
                ListImages = listImages,
                RelatedProducts = relatedProducts
            });
        }
    }
}
