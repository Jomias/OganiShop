using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OganiShop.Entities;
using OganiShop.Helpers;

namespace OganiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
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
    }
}
