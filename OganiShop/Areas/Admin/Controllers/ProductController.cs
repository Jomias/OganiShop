using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;
using OganiShop.Helpers;
using OganiShop.Models;
using OganiShop.Utils;
using System.Security.Claims;

namespace OganiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly OganiShopContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        public ProductController(OganiShopContext dbContext, IMapper mapper, IFileStorageService fileStorageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }
        public string GetAccount()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            if (claims != null)
            {
                var account = claims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (account != null)
                {
                    return account;
                }
            }
            return string.Empty;
        }
        public IActionResult Index()
        {
            var temp = _dbContext.Products
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Category)
                .Where(x => x.Category.IsDeleted == false).ToList();
            return View(temp);
        }


        public IActionResult AddOrEdit(int Id = 0)
        {
            ViewBag.Id = Id;

            var temp = _dbContext.Products.Find(Id);
            if (temp == null)
            {
                return View(new ProductModel());
            }

            return View(_mapper.Map<ProductModel>(temp));
        }

        [HttpPost]
        public IActionResult AddOrEdit(ProductModel model)
        {
            ViewBag.Id = model.Id == null ? 0 : model.Id;
            if (model.Slug == null && model.Name != null)
            {
                model.Slug = Slug.ToUrlSlug(model.Name);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var temp = _mapper.Map<Product>(model);
            if (model.Id == null)
            {
                _dbContext.Add(temp);
                _dbContext.SaveChanges();
                TempData["Message"] = "Add Product Successfully";
                return RedirectToAction("Index");
            }
            var account = GetAccount();

            temp.UpdatedDate = DateTime.Now;
            temp.UpdatedBy = account;
            _dbContext.Update(temp);
            _dbContext.SaveChanges();
            TempData["Message"] = "Edit Product Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            var entity = _dbContext.Products.Find(Id);
            if (entity == null)
            {
                TempData["Message"] = "Can't Remove";
                return RedirectToAction("Index");
            }
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = GetAccount();
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            TempData["Message"] = "Remove Product Successfully";
            return RedirectToAction("Index");

        }
    }
}
