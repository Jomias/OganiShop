using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiShop.Entities;
using OganiShop.Models;
using System;

namespace OganiShop.Controllers
{
    public class ContactController : Controller
    {
        private readonly OganiShopContext _dbContext;
        private readonly IMapper _mapper;

        public ContactController(OganiShopContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactMessageModel model)
        {
            model.Time = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _dbContext.Add(_mapper.Map<ContactMessage>(model));
            _dbContext.SaveChanges();
            TempData["SuccessMessage"] = "Message submitted successfully";
            return RedirectToAction("Index", "Home");
        }
    }
}
