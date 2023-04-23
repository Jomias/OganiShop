using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;
using OganiShop.Models;
using System;
using System.Security.Claims;

namespace OganiShop.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly OganiShopContext _dbContext;
        private readonly IMapper _mapper;
        public CheckOutController(OganiShopContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var account = HttpContext.Request.Cookies["account"];

            var query = _dbContext.ShoppingCarts
                .Where(x => x.IsDeleted == false && x.Customer == account && x.Status == 0)
                .Select(x => new
                {
                    x.Product.Id,
                    x.Product.Name,
                    Price = x.Product.Price - x.Product.Discount,
                    x.Quantity
                }).ToList();
            return View(query);
        }

        public IActionResult GetAddress()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            if (claims.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Json("");
            }
            var accClaims = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            var query = _dbContext.ShippingAddresses.Where(x => x.Account == accClaims);
            return query.Any() ? Json(query.ToList()) : Json("");
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

            int addressId = 0;
            var account = HttpContext.Request.Cookies["account"];
            if (account == null)
            {
                return View();
            }
            if (location.Id == null || location.Id == 0)
            {
                var entity = _mapper.Map<ShippingAddress>(location);
                _dbContext.Add(entity);
                _dbContext.SaveChanges();
                addressId = entity.Id;
            }
            var total = _dbContext.ShoppingCarts
                .Where(x => x.IsDeleted == false && x.Customer == account && x.Status == 0)
                .Select(x => new
                {
                    Price = x.Product.Price - x.Product.Discount,
                    x.Quantity,
                })
                .Sum(x => x.Price * x.Quantity);
            var order = new ShopOrder()
            {
                TotalPrice = total,
                AddressId = addressId,
                Account = account,
                CreatedDate = DateTime.Now,
                Status = 0,
                CreatedBy = account
            };
            _dbContext.Add(order);
            _dbContext.SaveChanges();

            var query = _dbContext.ShoppingCarts.Where(x => x.IsDeleted == false && x.Customer == account && x.Status == 0).Include(x => x.Product).ToList();
            var id = order.Id;
            foreach (var item in query)
            {
                _dbContext.OrderDetails.Add(new OrderDetail()
                {
                    ShopOrderId = id,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Price = item.Product.Price - item.Product.Discount,
                    CreatedDate = DateTime.Now,
                    CreatedBy = account,
                });
            }

            _dbContext.SaveChanges();

            var cart = _dbContext.ShoppingCarts
                .Where(x => x.Customer == account && x.IsDeleted == false && x.Status == 0)
                .ToList();

            foreach (var item in cart)
            {
                item.UpdatedBy = account;
                item.Status = 1;
                item.UpdatedDate = DateTime.Now;
            }
            _dbContext.UpdateRange(cart);
            _dbContext.SaveChanges();
            return Redirect("/Home/Index");
        }
    }
}
