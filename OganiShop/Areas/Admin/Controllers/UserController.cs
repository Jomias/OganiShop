using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    public class UserController : Controller
    {
        private readonly OganiShopContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "account";
        public UserController(OganiShopContext dbContext, IMapper mapper, IFileStorageService fileStorageService)
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
            var temp = _dbContext.Users.Where(x => x.IsDeleted == false).Include(x => x.AccountNavigation);
            return View(temp);
        }

        public IActionResult SetAdmin(int Id)
        {
            var entity = _dbContext.Users.Include(x => x.AccountNavigation).SingleOrDefault(x => x.Id == Id);
            if (entity == null)
            {
                TempData["Message"] = "Can't Set Admin";
                return RedirectToAction("Index");
            }

            entity.AccountNavigation.Role = "admin";
            entity.AccountNavigation.UpdatedBy = GetAccount();
            entity.AccountNavigation.UpdatedDate = DateTime.Now;
            _dbContext.SaveChanges();
            TempData["Message"] = "User role updated successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Profile()
        {
            var temp = _dbContext.Users.FirstOrDefault(x => x.Account == GetAccount());
            return View(_mapper.Map<UserUpdateModel>(temp));
        }

        [HttpPost]
        public async Task<IActionResult> Profile(UserUpdateModel model, IFormFile? ImageFile)
        {
            ViewBag.Id = model.Id;
            if (ImageFile != null)
            {
                if (model.Image == null)
                {
                    model.Image = await _fileStorageService.SaveFile(containerName, ImageFile);
                }
                else
                {
                    model.Image = await _fileStorageService.EditFile(containerName, ImageFile, model.Image);
                }
                ModelState["Image"].ValidationState = ModelValidationState.Valid;
                ModelState["Image"].RawValue = model.Image;
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var temp = _mapper.Map<User>(model);

            var account = GetAccount();

            temp.UpdatedDate = DateTime.Now;
            temp.UpdatedBy = account;
            _dbContext.Update(temp);
            _dbContext.SaveChanges();
            TempData["Message"] = "Edit User Successfully";
            return RedirectToAction("Index");

        }
    }
}
