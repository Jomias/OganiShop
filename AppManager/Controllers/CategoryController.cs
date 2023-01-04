using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AppManager.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;
        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCategoryCarousel()
        {
            var ctImg = (from b1 in _dbContext.CategoryEntities
                         join b2 in _dbContext.FileManageEntities
                         on b1.FileId equals b2.Id
                         select new CategoryCarouselModel()
                         {
                             Id = b1.FileId,
                             Name = b1.Name,
                             Slug = b1.Slug,
                             FileId = b1.FileId,
                             FilePath = b2.FilePath,
                         }).ToList();
            return Json(ctImg);
        }
        public IActionResult GetAllCategories()
        {
            var categories = _dbContext.CategoryEntities.Where(x => !x.IsDeleted);
            return Json(categories);
        }
    }
}
