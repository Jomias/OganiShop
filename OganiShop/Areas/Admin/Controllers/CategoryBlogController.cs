using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OganiShop.Entities;
using OganiShop.Helpers;
using OganiShop.Models;
using OganiShop.Utils;
using System.Security.Claims;

namespace OganiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CategoryBlogController : Controller
    {
        private readonly OganiShopContext _dbContext;
        private readonly IMapper _mapper;
        public CategoryBlogController(OganiShopContext dbContext, IMapper mapper, IFileStorageService fileStorageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var temp =_dbContext.CategoryBlogs.Where(x => x.IsDeleted == false).ToList();
            return View(temp);
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
        public IActionResult AddOrEdit(int Id = 0)
        {
            ViewBag.Id = Id;
            var temp = _dbContext.CategoryBlogs.Find(Id);
            if (temp == null)
            {
                return View(new CategoryBlogModel());
            }

            return View(_mapper.Map<CategoryBlogModel>(temp));
        }
        public IActionResult Delete(int Id)
        {
            var entity = _dbContext.CategoryBlogs.Find(Id);
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
            TempData["Message"] = "Remove CategoryBlog Successfully";
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult AddOrEdit(CategoryBlogModel model)
        {
            ViewBag.Id = model.Id == null ? 0 : model.Id;
            if (model.Slug == null && model.Name != null)
            {
                model.Slug = Slug.ToUrlSlug(model.Name);
                ModelState.SetModelValue("Slug", new ValueProviderResult(model.Slug));

            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var temp = _mapper.Map<CategoryBlog>(model);
            if (model.Id == null)
            {
                _dbContext.Add(temp);
                _dbContext.SaveChanges();
                TempData["Message"] = "Add CategoryBlog Successfully";
                return RedirectToAction("Index");
            }
            var account = GetAccount();

            temp.UpdatedDate = DateTime.Now;
            temp.UpdatedBy = account;
            _dbContext.Update(temp);
            _dbContext.SaveChanges();
            TempData["Message"] = "Edit CategoryBlog Successfully";
            return RedirectToAction("Index");
        }
    }
}
