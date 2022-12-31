using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppManager.Common;

namespace AppManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public AccountController(AppDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }

        public IActionResult Login(string ReturnUrl)
        {
            var cl = HttpContext.User.Identity as ClaimsIdentity;
            if (cl.Claims.Any())
            {
                var role = cl.Claims.Where(c => c.Type == ClaimTypes.Role)
                                    .Select(c => c.Value).SingleOrDefault();
                if (role == "customer")
                {
                    return Redirect("/Home/Index");
                }
                var returnUrl = string.IsNullOrEmpty(ReturnUrl) ? "/Admin/Home/Index" : ReturnUrl;
                return Redirect(returnUrl);
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountManagerModel model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                TempData["Error"] = error.FirstOrDefault();
                return Redirect("/Admin/Account/Login");
            }
            var query = _dbContext.AccountManagerEntities.Where(a => a.Account == model.Account && a.Password == Security.EncryptPlainTextToCipherText(model.Password))
                                                  .Select(a => new AccountManagerModel()
                                                  {
                                                      Account = a.Account,
                                                      Password = a.Password,
                                                      Role = a.Role
                                                  }).ToList();
            if (query.Any())
            {
                var account = query.FirstOrDefault();
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Account)
                };
                claims.Add(new Claim(ClaimTypes.Role, account.Role));
                var claimIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIndentity));
                HttpContext.Response.Cookies.Append("account", account.Account);

                if (account.Role == "customer")
                {
                    return Redirect("/Home/Index");
                }
                var returnUrl = string.IsNullOrEmpty(model.ReturnUrl) ? "/Admin/Home/Index" : model.ReturnUrl;
                return Redirect(returnUrl);
            }
            TempData["Error"] = "Tài khoản hoặc mật khẩu không chính xác!";
            return Redirect("/Admin/Account/Login");
        }
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete("Account");
            return Redirect("/Admin/Account/Login");
        }
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SignUp(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                TempData["Error"] = error.FirstOrDefault();
                return Redirect("/Admin/Account/Signup");
            }
            var query = _dbContext.UserEntities.Where(x => x.Account == user.Account || x.Email == user.Email)
                                               .Select(x => new UserEntity() { });
            if (query.Any())
            {
                TempData["Error"] = "Đã tồn tại tài khoản này!";
                return Redirect("/Admin/Account/Signup");
            }

            _dbContext.AccountManagerEntities.Add(new AccountManagerEntity()
            {
                Account = user.Account,
                Password = Security.EncryptPlainTextToCipherText(user.Password),
                Role = "customer",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });
            _dbContext.SaveChanges();
            _dbContext.UserEntities.Add(new UserEntity()
            {
                Account = user.Account,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });
            _dbContext.SaveChanges();
            _dbContext.AccountImageEntities.Add(new AccountImageEntity()
            {
                Account = user.Account,
                FileId = user.AvatarId,
                IsAvatar = true,
                CreatedDate = DateTime.Now,
                CreatedBy = user.Account,
                UpdatedDate = DateTime.Now,
                UpdatedBy = user.Account,
            });
            _dbContext.SaveChanges();
            return Redirect("/Admin/Account/Login");
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file == null)
            {
                return Json(new { status = "error" });
            }
            string folderUploads = Path.Combine(_environment.WebRootPath, "img\\user-avatar");
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string fullPath = Path.Combine(folderUploads, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var fileEntity = new FileManageEntity()
            {
                Name = file.Name,
                FilePath = "/img/user-avatar/" + fileName,
                FileType = "image",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            _dbContext.FileManageEntities.Add(fileEntity);
            var status = _dbContext.SaveChanges();
            if (status == 0)
            {
                return Json(new { status = "error" });
            }
            var model = new FileModel()
            {
                Id = fileEntity.Id,
                Name = fileEntity.Name,
                FilePath = fileEntity.FilePath,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };
            return Json(new
            {
                status = "success",
                fileInfo = model
            });
        }

    }
}
