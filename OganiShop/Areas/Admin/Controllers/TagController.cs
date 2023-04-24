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
    public class TagController : Controller
    {
        private readonly OganiShopContext _dbContext;
        private readonly IMapper _mapper;
        public TagController(OganiShopContext dbContext, IMapper mapper, IFileStorageService fileStorageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var temp =_dbContext.Tags.Where(x => x.IsDeleted == false).ToList();
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
            var temp = _dbContext.Tags.Find(Id);
            if (temp == null)
            {
                return View(new TagModel());
            }

            return View(_mapper.Map<TagModel>(temp));
        }
        public IActionResult Delete(int Id)
        {
            var entity = _dbContext.Tags.Find(Id);
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
            TempData["Message"] = "Remove Tag Successfully";
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult AddOrEdit(TagModel model)
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

            var temp = _mapper.Map<Tag>(model);
            if (model.Id == null)
            {
                _dbContext.Add(temp);
                _dbContext.SaveChanges();
                TempData["Message"] = "Add Tag Successfully";
                return RedirectToAction("Index");
            }
            var account = GetAccount();

            temp.UpdatedDate = DateTime.Now;
            temp.UpdatedBy = account;
            _dbContext.Update(temp);
            _dbContext.SaveChanges();
            TempData["Message"] = "Edit Tag Successfully";
            return RedirectToAction("Index");
        }
    }
}
