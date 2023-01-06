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
        public string GetAccount()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            var account = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            return account;
        }
        public IActionResult UserProfile(string account)
        {
            ViewBag.Account = GetAccount();
            ViewBag.Role = _dbContext.AccountManagerEntities.First(x => x.Account == GetAccount()).Role;
            var acc = string.IsNullOrEmpty(account) ? GetAccount() : account;
            var user = (from b1 in _dbContext.UserEntities
                        join b2 in _dbContext.AccountImageEntities on b1.Account equals b2.Account
                        join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                        join b4 in _dbContext.AccountManagerEntities on b1.Account equals b4.Account
                        where !b2.IsDeleted && b2.IsAvatar && b1.Account == acc
                        select new UserModel
                        {
                            Account = acc,
                            FirstName = b1.FirstName,
                            LastName = b1.LastName,
                            Phone = b1.Phone,
                            Email = b1.Email,
                            AvatarId = b3.Id,
                            AvatarPath = b3.FilePath,
                            Role = b4.Role,
                        }).First();
            return View(user);
        }

        public IActionResult UpdateAvatar(string account, int oldId, int newId)
        {
            var oldImage = _dbContext.AccountImageEntities.First(x => x.FileId == oldId);
            oldImage.IsAvatar = false;
            oldImage.UpdatedDate = DateTime.Now;
            oldImage.UpdatedBy = account;
            _dbContext.AccountImageEntities.Update(oldImage);
            var newImage = new AccountImageEntity()
            {
                Account = account,
                FileId = newId,
                IsAvatar = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Status = 0,
                CreatedBy = account,
                UpdatedBy = account,
            };
            _dbContext.AccountImageEntities.Add(newImage);
            _dbContext.SaveChanges();
            return Redirect("/Admin/Account/UserProfile");
        }

        [HttpPost]
        public IActionResult UserProfile(UserModel model)
        {
            var account = _dbContext.AccountManagerEntities.First(x => x.Account == model.Account);
            account.Role = model.Role;
            account.UpdatedDate = DateTime.Now;
            account.UpdatedBy = model.Account;
            _dbContext.AccountManagerEntities.Update(account);
            var entity = _dbContext.UserEntities.First(x => x.Account == model.Account);
            entity.Account = model.Account;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Phone = model.Phone;
            entity.Email = model.Email;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = GetAccount();

            _dbContext.UserEntities.Update(entity);
            _dbContext.SaveChanges();
            return UserProfile(model.Account);
        }


        public IActionResult Login(string ReturnUrl)
        {
            var claim = HttpContext.User.Identity as ClaimsIdentity;
            if (!claim.Claims.Any())
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
            if (!query.Any())
            {
                TempData["Error"] = "Tài khoản hoặc mật khẩu không chính xác!";
                return Redirect("/Admin/Account/Login");
            }
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
                UpdatedDate = DateTime.Now,
                CreatedBy = user.Account,
                UpdatedBy = user.Account,
                Status = 0,
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
                UpdatedDate = DateTime.Now,
                CreatedBy = user.Account,
                UpdatedBy = user.Account,
                Status = 0,
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
                Status = 0,
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
                CreatedBy = GetAccount(),
                UpdatedBy = GetAccount(),
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
                Status = 0,
            };
            return Json(new
            {
                status = "success",
                fileInfo = model
            });
        }

    }
}
