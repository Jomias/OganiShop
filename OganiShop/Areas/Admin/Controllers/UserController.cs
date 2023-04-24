using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;
using OganiShop.Helpers;
using OganiShop.Models;
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
            return View(new UserModel()
            {
                Id = temp.Id,
                FirstName = temp.FirstName,
                LastName = temp.LastName,
                Phone = temp.Phone,
                Email = temp.Email,
                Image = temp.Image,
                Account = GetAccount(),
            });
        }
    }
}
