using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AppManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ReportController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ReportController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCategoryOrderQuantity()
        {
            var query = (from b1 in _dbContext.ProductEntities
                         join b2 in _dbContext.OrderDetailEntities on b1.Id equals b2.ProductId
                         join b3 in _dbContext.CategoryEntities on b1.CategoryId equals b3.Id
                         where !b2.IsDeleted
                         group new { b1, b2, b3 } by new { b3.Id, b3.Name } into b5
                         select new
                         {
                             label = b5.Key.Name,
                             value = b5.Sum(x => x.b2.Quantity)
                         }).ToList();
            return Json(query);
        }

        public IActionResult GetRevenue()
        {
            var query = (from b1 in _dbContext.ShopOrderEntities
                         where b1.OrderStatus == 2 || b1.OrderStatus == 3
                         group new {b1} by new { b1.CreatedDate.Date } into b2
                         select new
                         {
                              y = b2.Key.Date.ToString("yyyy-MM-dd"),
                              Revenue = b2.Sum(x => x.b1.TotalPrice)
                         }).ToList();
            return Json(query);
        }
        public IActionResult MonthlyCategoryReport()
        {
            var query = (from b1 in _dbContext.ProductEntities
                         join b2 in _dbContext.OrderDetailEntities on b1.Id equals b2.ProductId
                         join b3 in _dbContext.ShopOrderEntities on b2.ShopOrderId equals b3.Id
                         join b4 in _dbContext.CategoryEntities on b1.CategoryId equals b4.Id
                         where b3.CreatedDate.Month == DateTime.Now.Month
                         group new { b1, b2, b3, b4 } by new { b4.Id, b4.Name, b4.Slug } into b5
                         select new
                         {
                             b5.Key.Id,
                             b5.Key.Name,
                             b5.Key.Slug,
                             Quantity = b5.Sum(x => x.b2.Quantity)
                         }).ToList();
            return Json(query.OrderByDescending(x => x.Name));
        }

        public IActionResult AnnuallyCategoryReport()
        {
            var categories = (from b1 in _dbContext.ProductEntities
                              join b2 in _dbContext.OrderDetailEntities on b1.Id equals b2.ProductId
                              join b3 in _dbContext.ShopOrderEntities on b2.ShopOrderId equals b3.Id
                              join b4 in _dbContext.CategoryEntities on b1.CategoryId equals b4.Id
                              where b3.CreatedDate.Year == DateTime.Now.Year
                              where b3.OrderStatus == 2 || b3.OrderStatus == 3
                              group new { b1, b2, b3, b4 } by new { b4.Name, b3.CreatedDate } into b5
                              select new
                              {
                                  Category = b5.Key.Name,
                                  Month = b5.Key.CreatedDate.Month,
                                  Quantity = b5.Sum(x => x.b2.Quantity)
                              }).ToList();
            Dictionary<string, List<int>> result = new Dictionary<string, List<int>>();
            foreach (var category in categories)
            {
                if (!result.ContainsKey(category.Category))
                {
                    var month = categories.Where(x => x.Category == category.Category).ToList();
                    result[category.Category] = Enumerable.Repeat(0, 12).ToList(); ;
                    foreach (var m in month)
                    {
                        result[category.Category][m.Month - 1] = m.Quantity;
                    }
                }
            }
            List<AnuallyReportModel> report = new List<AnuallyReportModel>();
            foreach (var item in result)
            {
                report.Add(new AnuallyReportModel
                {
                    Category = item.Key,
                    Month = item.Value,
                });
            }
            return Json(report.OrderByDescending(x => x.Category));
        }
    }
}
