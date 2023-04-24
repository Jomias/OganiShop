using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;
using OganiShop.Helpers;
using OganiShop.Models;
using OganiShop.Utils;
using System.Linq;
using System.Security.Claims;

namespace OganiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class BlogTagController : Controller
    {
        private readonly OganiShopContext _dbContext;
        private readonly IMapper _mapper;
        public BlogTagController(OganiShopContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IActionResult Index(int Id = 0)
        {
            if (_dbContext.Blogs.FirstOrDefault(x => x.Id == Id && x.IsDeleted == false) == null)
            {
                return BadRequest();
            }
            ViewBag.Id = Id;
            var temp =_dbContext.BlogTags.Where(x => x.IsDeleted == false && x.BlogId == Id).Include(x => x.Tag)
                .Where(x => x.Tag.IsDeleted == false).ToList();
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
        public IActionResult AddOrEdit(int Id = 0, int BlogId = 0)
        {
            ViewBag.Id = Id;
            ViewBag.BlogId = BlogId;
            var temp = _dbContext.BlogTags.Find(Id);
            if (temp == null)
            {
                var temp1 = new BlogTagModel();
                if (BlogId != 0) 
                {
                    temp1.BlogId = BlogId;
                }
                return View(temp1);
            }
            return View(_mapper.Map<BlogTagModel>(temp));
        }
        public IActionResult Delete(int Id)
        {

            var entity = _dbContext.BlogTags.Find(Id);
            if (entity == null) 
            {
                TempData["Message"] = "Can't Remove";
                return RedirectToAction("Index", new { Id });
            }
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = GetAccount();
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            TempData["Message"] = "Remove BlogTag Successfully";
            return RedirectToAction("Index", new { Id = entity.BlogId });
        }
        [HttpPost]
        public IActionResult AddOrEdit(BlogTagModel model)
        {
            ViewBag.Id = model.Id == null ? 0 : model.Id;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var temp = _mapper.Map<BlogTag>(model);
            if (model.Id == null)
            {
                _dbContext.Add(temp);
                _dbContext.SaveChanges();

                TempData["Message"] = "Add BlogTag Successfully";
                return RedirectToAction("Index", new { Id = model.BlogId});
            }
            var account = GetAccount();

            temp.UpdatedDate = DateTime.Now;
            temp.UpdatedBy = account;
            _dbContext.Update(temp);
            _dbContext.SaveChanges();
            TempData["Message"] = "Edit BlogTag Successfully";
            return RedirectToAction("Index", new { Id = model.BlogId });

        }
    }
}
