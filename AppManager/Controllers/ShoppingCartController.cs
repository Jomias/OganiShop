using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AppManager.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly AppDbContext _dbContext;
        public ShoppingCartController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var account = HttpContext.Request.Cookies["account"];
            var query = (from b1 in _dbContext.ProductEntities
                         join b2 in _dbContext.DiscountEntities on b1.Id equals b2.ProductId into lp
                         from prd in lp.DefaultIfEmpty()
                         where !b1.IsDeleted
                         select new ProductModel()
                         {
                             Id = b1.Id,
                             Name = b1.Name,
                             Slug = b1.Slug,
                             Price = (prd.CreatedDate <= DateTime.Now && prd.EndDate >= DateTime.Now)
                                     ? (prd.DiscountType == 0 ? (b1.Price - prd.DiscountValue) : (b1.Price * (1 - prd.DiscountValue / 100)))
                                     : b1.Price,
                         });

            var cart = (from b1 in query
                        join b2 in _dbContext.ProductImageEntities on b1.Id equals b2.ProductId
                        join b3 in _dbContext.FileManageEntities on b2.FileId equals b3.Id
                        join b4 in _dbContext.ShoppingCartEntities on b1.Id equals b4.ProductId
                        where !b4.IsDeleted && b2.IsAvatar && !b2.IsDeleted
                        where b4.Customer == account && b4.Status == 0
                        select new ProductModel()
                        {
                            Id = b1.Id,
                            CartId = b4.Id,
                            Name = b1.Name,
                            Slug = b1.Slug,
                            Price = b1.Price,
                            Quantity = b4.Quantity,
                            Avatar = b3.FilePath,
                            AvatarFileId = b3.Id
                        }).ToList();

            return View(cart);
        }


        [HttpPost]
        public IActionResult AddOrUpdateCartItem(int id, int quantity)
        {
            var account = HttpContext.Request.Cookies["account"];
            if (account == null)
            {
                account = Guid.NewGuid().ToString();
                HttpContext.Response.Cookies.Append("account", account);
            }
            var cart = _dbContext.ShoppingCartEntities
                .Where(x => x.ProductId == id && x.Customer == account && x.Status == 0)
                .Select(x => new ShoppingCartEntity()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status,
                    IsDeleted = x.IsDeleted,
                    Customer = account
                });

            if (cart.Any())
            {
                var item = cart.Select(x => new ShoppingCartEntity()
                {
                    Id = x.Id,
                    ProductId = id,
                    Quantity = quantity == 0 ? x.Quantity + 1 : quantity,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    Status = x.Status,
                    IsDeleted = x.IsDeleted,
                    Customer = x.Customer
                }).First();
                _dbContext.ShoppingCartEntities.Update(item);
            }
            else
            {
                _dbContext.ShoppingCartEntities.Add(new ShoppingCartEntity()
                {
                    ProductId = id,
                    Quantity = quantity == 0 ? 1 : quantity,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = account,
                    UpdatedBy = account,
                    Status = 0,
                    IsDeleted = false,
                    Customer = account
                });
            }
            _dbContext.SaveChanges();
            return Json("ok!");
        }

        [HttpPost]
        public IActionResult RemoveItem(int id)
        {
            var cart = _dbContext.ShoppingCartEntities.Find(id);
            cart.IsDeleted = true;
            cart.Status = 1;
            _dbContext.ShoppingCartEntities.Update(cart);
            _dbContext.SaveChanges();
            return Redirect("/ShoppingCart/Index");
        }
    }
}
