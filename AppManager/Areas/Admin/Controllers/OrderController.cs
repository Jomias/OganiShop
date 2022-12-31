using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AppManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin,staff")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _dbContext;

        public OrderController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int pageNumber = 1)
        {
            int pageSize = 5;
            var query = (from b1 in _dbContext.ShopOrderEntities
                         join b2 in _dbContext.AccountManagerEntities on b1.Account equals b2.Account into tbl
                         from t in tbl.DefaultIfEmpty()
                         where !b1.IsDeleted
                         select new ShopOrderModel()
                         {
                             ShopOrderId = b1.Id,
                             OrderStatus = b1.OrderStatus,
                             TotalPrice = b1.TotalPrice,
                             Account = t.Account == null ? null : b1.Account,
                             CreatedDate = b1.CreatedDate,
                         }).ToList();
            var total = query.Count();
            ViewBag.pageCount = Math.Ceiling((decimal)total / pageSize);
            ViewBag.pageNumber = pageNumber;
            ViewBag.pageSize = pageSize;
            var listCategory = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return View(query);
        }

        public IActionResult OrderDetail(int id)
        {
            var order = (from b1 in _dbContext.ShopOrderEntities
                         join b2 in _dbContext.AccountManagerEntities on b1.Account equals b2.Account into tbl
                         from t in tbl.DefaultIfEmpty()
                         where !b1.IsDeleted && b1.Id == id
                         select new ShopOrderModel()
                         {
                             ShopOrderId = b1.Id,
                             OrderStatus = b1.OrderStatus,
                             TotalPrice = b1.TotalPrice,
                             Account = t.Account == null ? null : b1.Account,
                             CreatedDate = b1.CreatedDate,
                         }).First();
            var orderDetail = (from b1 in _dbContext.ShopOrderEntities
                               join b2 in _dbContext.OrderDetailEntities on b1.Id equals b2.ShopOrderId
                               join b3 in _dbContext.ProductEntities on b2.ProductId equals b3.Id
                               where !b1.IsDeleted && !b2.IsDeleted
                               where b1.Id == id
                               select new OrderDetailModel()
                               {
                                   ShopOrderId = b1.Id,
                                   ProductId = b2.ProductId,
                                   ProductName = b3.Name,
                                   Quantity = b2.Quantity,
                                   Price = b2.Price,
                                   TotalPrice = b2.TotalPrice
                               }).ToList();
            return View(new OrderInfoModel()
            {
                ShopOrder = order,
                ListOrderDetail = orderDetail,
            });
        }
        public IActionResult Delete(int id)
        {
            var entity = _dbContext.ShopOrderEntities.Find(id);
            entity.IsDeleted = true;
            _dbContext.ShopOrderEntities.Update(entity);
            var detail = _dbContext.OrderDetailEntities.Where(x => x.ShopOrderId == id).Select(x => x).ToList();
            foreach (var item in detail)
            {
                item.IsDeleted = true;
                _dbContext.OrderDetailEntities.Update(item);
            }
            _dbContext.SaveChanges();
            return Redirect("/Admin/Order/Index");
        }
    }

}
