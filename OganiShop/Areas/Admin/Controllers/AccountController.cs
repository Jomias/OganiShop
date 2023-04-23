using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OganiShop.Entities;
using OganiShop.Models;
using OganiShop.Utils;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using OganiShop.Helpers;

namespace OganiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly OganiShopContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "account";
        public AccountController(OganiShopContext dbContext, IMapper mapper, IFileStorageService fileStorageService)
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
            return View();
        }

        [AllowAnonymous]

        public IActionResult Login(string ReturnUrl)
        {
            var claim = HttpContext.User.Identity as ClaimsIdentity;
            if (claim == null || !claim.Claims.Any())
            {
                ViewBag.ReturnUrl = ReturnUrl;
                return View();
            }
            var role = claim.Claims.Where(c => c.Type == ClaimTypes.Role)
                                .Select(c => c.Value).SingleOrDefault();
            if (role == "customer")
            {
                return Redirect("/Home/Index");
            }
            var returnUrl = string.IsNullOrEmpty(ReturnUrl) ? "/Admin/Home/Index" : ReturnUrl;
            return Redirect(returnUrl);
        }

        [AllowAnonymous]

        [HttpPost]
        public async Task<IActionResult> Login(AccountManagerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var accountManager = await _dbContext.AccountManagers
                .SingleOrDefaultAsync(a => a.Account == model.Account && a.Password == Security.EncryptPlainTextToCipherText(model.Password));

            if (accountManager == null)
            {
                TempData["ErrorMessage"] = "Tài khoản hoặc mật khẩu không chính xác!";
                return Redirect("/Admin/Account/Login");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, accountManager.Account)
            };
            claims.Add(new Claim(ClaimTypes.Role, accountManager.Role));
            var claimIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIndentity));
            HttpContext.Response.Cookies.Append("account", accountManager.Account);

            if (accountManager.Role == "customer")
            {
                return Redirect("/Home/Index");
            }
            var returnUrl = string.IsNullOrEmpty(model.ReturnUrl) ? "/Admin/Home/Index" : model.ReturnUrl;
            return Redirect(returnUrl);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("Account");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var query = _dbContext.Users.Where(x => x.Account == user.Account || x.Email == user.Email)
                                               .Select(x => new User() { });
            if (query.Any())
            {
                TempData["ErrorMessage"] = "Đã tồn tại tài khoản này!"; 
                return Redirect("/Admin/Account/Register");
            }

            if (user.ImageFile != null)
            {
                user.Image = await _fileStorageService.SaveFile(containerName, user.ImageFile);
            }
            user.CreatedBy = user.Account;
            _dbContext.AccountManagers.Add(new AccountManager()
            {
                Account = user.Account,
                Password = Security.EncryptPlainTextToCipherText(user.Password),
                Role = "customer",
                CreatedBy = user.Account,
            });
            _dbContext.SaveChanges();
           
            _dbContext.Users.Add(_mapper.Map<User>(user));
            _dbContext.SaveChanges();

            return Redirect("/Admin/Account/Login");
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("Account");
            return Redirect("/Admin/Account/Login");
        }
    }
}
