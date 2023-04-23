using Microsoft.AspNetCore.Mvc;
using OganiShop.Entities;
using System;

namespace OganiShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly OganiShopContext _dbContext;
        public ShoppingCartController(OganiShopContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var account = HttpContext.Request.Cookies["account"];
            var cart = _dbContext.ShoppingCarts
                .Where(x => x.IsDeleted == false && x.Customer == account && x.Status == 0)
                .Select(x => new
                {
                    x.Product.Id,
                    CartId = x.Id,
                    x.Product.Name,
                    x.Product.Slug,
                    x.Product.Image,
                    Price = x.Product.Price - x.Product.Discount,
                    x.Quantity
                }).ToList();

            return View(cart);
        }


        [HttpPost]
        public IActionResult AddOrUpdateCartItem(int id, int quantity)
        {
            try
            {
                if (id <= 0 || quantity < 0)
                {
                    return BadRequest("Invalid input parameters.");
                }

                var account = HttpContext.Request.Cookies["account"];
                if (string.IsNullOrEmpty(account))
                {
                    account = Guid.NewGuid().ToString();
                    HttpContext.Response.Cookies.Append("account", account);
                }

                var cart = _dbContext.ShoppingCarts
                    .SingleOrDefault(x => x.ProductId == id && x.Customer == account && x.IsDeleted == false);

                if (cart != null)
                {
                    cart.Quantity = quantity == 0 ? cart.Quantity + 1 : quantity;
                    cart.UpdatedDate = DateTime.UtcNow;
                    cart.UpdatedBy = account;
                    _dbContext.Update(cart);
                }
                else
                {
                    var newCart = new ShoppingCart()
                    {
                        ProductId = id,
                        Quantity = quantity == 0 ? 1 : quantity,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        CreatedBy = account,
                        Customer = account
                    };
                    _dbContext.Add(newCart);
                }

                _dbContext.SaveChanges();

                return Json("ok!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        [HttpPost]
        public IActionResult RemoveItem(int id)
        {
            var cart = _dbContext.ShoppingCarts.Find(id);
            cart.IsDeleted = true;
            _dbContext.ShoppingCarts.Update(cart);
            _dbContext.SaveChanges();
            return Json("ok!");

        }
    }
}
