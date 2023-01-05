using AppManager.Entities;
using AppManager.Models; 
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace AppManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly AppDbContext _dbContext;

        public UserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(string name, string role, int pageNumber = 1)
        {
                int pageSize = 5;
                var query = (from b1 in _dbContext.AccountManagerEntities
                             join b2 in _dbContext.AccountImageEntities on b1.Account equals b2.Account
                             join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                             join b4 in _dbContext.UserEntities on b1.Account equals b4.Account
                             where string.IsNullOrEmpty(name) || (b4.FirstName + b4.LastName).Trim().ToLower().Contains(name.Trim().ToLower())
                             where string.IsNullOrEmpty(role) || b1.Role == role
                             where b2.IsAvatar && !b2.IsDeleted && !b4.IsDeleted
                             select new UserModel()
                             {
                                 Account = b1.Account,
                                 FirstName = b4.FirstName,
                                 LastName = b4.LastName,
                                 Role = b1.Role,
                                 AvatarId = b3.Id,
                                 AvatarPath = b3.FilePath
                             }).ToList();
                var total = query.Count();
                ViewBag.pageCount = Math.Ceiling((decimal)total / pageSize);
                ViewBag.pageNumber = pageNumber;
                ViewBag.pageSize = pageSize;
                ViewBag.name = name;
                ViewBag.role = role;
                var temp = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                return View(temp);
        }
    }
}
