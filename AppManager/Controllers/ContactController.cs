using AppManager.Entities;
using AppManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AppManager.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _dbContext;
        public ContactController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendContact(ContactMessageModel model)
        {
            var contactMes = new ContactMessageEntity()
            {
                Name = model.Name,
                Email = model.Email,
                Message = model.Message,
                Time = DateTime.Now,
            };
            _dbContext.ContactMessageEntities.Add(contactMes);
            _dbContext.SaveChanges();
            return Redirect("/Home/Index");
        }
    }
}
