using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;
using System.Security.Claims;

namespace OganiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private readonly OganiShopContext _dbContext;
        private readonly IMapper _mapper;
        public OrderController(OganiShopContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
            var temp = _dbContext.ShopOrders.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate).OrderBy(x => x.Status).ToList();
            return View(temp);
        }
        public IActionResult Detail(int Id)
        {
            var temp = _dbContext.ShopOrders
                                .Include(x => x.Address)
                                .Include(x => x.OrderDetails.Where(order => order.IsDeleted == false))
                                .ThenInclude(x => x.Product)
                                .SingleOrDefault(x => x.IsDeleted == false && x.Id == Id);

            if (temp == null) 
            {
                return NotFound();
            }
            return View(temp);
        }


        public IActionResult Accepted(int Id)
        {

            var entity = _dbContext.ShopOrders.Include(x => x.OrderDetails.Where(x => x.IsDeleted == false)).SingleOrDefault(x => x.Id == Id);
            if (entity == null)
            {
                TempData["Message"] = "Problem happened";
                return RedirectToAction("Index");
            }
            entity.Status = 1;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = GetAccount();
            _dbContext.Update(entity);
            _dbContext.SaveChanges();

            foreach (var orderDetail in entity.OrderDetails)
            {
                var product = _dbContext.Products.Find(orderDetail.ProductId);
                if (product != null)
                {
                    product.Quantity -= orderDetail.Quantity;
                    product.UpdatedBy = GetAccount();
                    product.UpdatedDate = DateTime.Now;
                    _dbContext.Update(product);
                }
            }
            _dbContext.SaveChanges();
            TempData["Message"] = "Change Status Successfully";
            return RedirectToAction("Index");
        }


        public IActionResult Shipping(int Id)
        {

            var entity = _dbContext.ShopOrders.Find(Id);
            if (entity == null)
            {
                TempData["Message"] = "Problem happened";
                return RedirectToAction("Index");
            }
            entity.Status = 2;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = GetAccount();
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            TempData["Message"] = "Change Status Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Done(int Id)
        {

            var entity = _dbContext.ShopOrders.Find(Id);
            if (entity == null)
            {
                TempData["Message"] = "Problem happened";
                return RedirectToAction("Index");
            }
            entity.Status = 3;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = GetAccount();
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            TempData["Message"] = "Change Status Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {

            var entity = _dbContext.ShopOrders.Find(Id);
            if (entity == null)
            {
                TempData["Message"] = "Can't Remove";
                return RedirectToAction("Index");
            }
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = GetAccount();
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            TempData["Message"] = "Remove Category Successfully";
            return RedirectToAction("Index");
        }
    }
}
