using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace AppManager.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly AppDbContext _dbContext;
        public CheckOutController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var account = HttpContext.Request.Cookies["account"];
            var query = (from b1 in _dbContext.ShoppingCartEntities
                         join b2 in _dbContext.ProductEntities on b1.ProductId equals b2.Id into tbl1
                         from t1 in tbl1.DefaultIfEmpty()
                         join b3 in _dbContext.DiscountEntities on t1.Id equals b3.ProductId into tbl2
                         from t2 in tbl2.DefaultIfEmpty()
                         where !b1.IsDeleted && b1.Customer == account && b1.Status == 0
                         select new ProductModel()
                         {
                             Id = t1.Id,
                             Name = t1.Name,
                             Price = (t2.CreatedDate <= DateTime.Now && t2.EndDate >= DateTime.Now)
                                     ? (t2.DiscountType == 0 ? (t1.Price - t2.DiscountValue) : (t1.Price * (1 - t2.DiscountValue / 100)))
                                     : t1.Price,
                             Quantity = b1.Quantity
                         }).ToList();
            return View(query);
        }

        [HttpPost]
        public IActionResult Index(ShippingAddressModel location)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                TempData["Error"] = error.FirstOrDefault();
                return Redirect("/CheckOut/Index");
            }
            var account = HttpContext.Request.Cookies["account"];
            int addressId = location.Id;
            if (addressId == 0)
            {
                var entity = new ShippingAddressEntity()
                {
                    Account = account,
                    FirstName = location.FirstName,
                    LastName = location.LastName,
                    Address = location.Address,
                    City = location.City,
                    Country = location.Country,
                    Phone = location.Phone,
                    PostCode = location.PostCode,
                    Note = location.Note,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = account,
                    UpdatedBy = account
                };
                _dbContext.ShippingAddressEntities.Add(entity);
                _dbContext.SaveChanges();
                addressId = entity.Id;
            }

            var query = (from a in _dbContext.ShoppingCartEntities
                         join b in _dbContext.ProductEntities on a.ProductId equals b.Id
                         join c in _dbContext.DiscountEntities on b.Id equals c.ProductId into tbl
                         from t in tbl.DefaultIfEmpty()
                         where !a.IsDeleted && a.Customer == account
                         where a.Status == 0
                         select new ProductModel()
                         {
                             Id = b.Id,
                             Name = b.Name,
                             Price = (t.CreatedDate <= DateTime.Now && t.EndDate >= DateTime.Now)
                                     ? (t.DiscountType == 0 ? (b.Price - t.DiscountValue) : (b.Price * (1 - t.DiscountValue / 100)))
                                     : b.Price,
                             Quantity = a.Quantity
                         }).ToList();

            decimal total = 0;
            foreach (var item in query)
            {
                total += item.Price * item.Quantity;
            }

            var order = new ShopOrderEntity()
            {
                OrderStatus = 1,
                TotalPrice = total,
                AddressId = addressId,
                Account = account,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = account,
                UpdatedBy = account
            };
            _dbContext.ShopOrderEntities.Add(order);
            _dbContext.SaveChanges();

            foreach (var item in query)
            {
                var id = order.Id;
                _dbContext.OrderDetailEntities.Add(new OrderDetailEntity()
                {
                    ShopOrderId = id,
                    ProductId = item.Id,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    TotalPrice = item.Price * item.Quantity,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = account,
                    UpdatedBy = account
                });
            }
            _dbContext.SaveChanges();

            var cart = _dbContext.ShoppingCartEntities.Where(x => x.Customer == account && !x.IsDeleted)
                                                      .Select(x => new ShoppingCartEntity()
                                                      {
                                                          Id = x.Id,
                                                          ProductId = x.ProductId,
                                                          Quantity = x.Quantity,
                                                          Customer = x.Customer,
                                                          CreatedDate = x.CreatedDate,
                                                          UpdatedDate = DateTime.Now,
                                                          CreatedBy = x.CreatedBy,
                                                          UpdatedBy = x.UpdatedBy,
                                                          Status = 1,
                                                          IsDeleted = x.IsDeleted,
                                                      }).ToList();
            _dbContext.ShoppingCartEntities.UpdateRange(cart);
            _dbContext.SaveChanges();
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public IActionResult GetAddress()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            if (claims.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Json("");
            }
            var accClaims = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            var query = _dbContext.ShippingAddressEntities.Where(x => x.Account == accClaims)
                                                      .Select(x => new ShippingAddressEntity()
                                                      {
                                                          Id = x.Id,
                                                          Account = accClaims,
                                                          FirstName = x.FirstName,
                                                          LastName = x.LastName,
                                                          Address = x.Address,
                                                          City = x.City,
                                                          Country = x.Country,
                                                          Phone = x.Phone,
                                                          PostCode = x.PostCode,
                                                          Note = x.Note,
                                                          CreatedDate = DateTime.Now,
                                                          UpdatedDate = DateTime.Now,
                                                          CreatedBy = x.CreatedBy,
                                                          UpdatedBy = x.UpdatedBy
                                                      });
            return query.Any() ? Json(query.ToList()) : Json("");
        }

    }
}
