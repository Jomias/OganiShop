using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OganiShop.Entities;

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
        public IActionResult Index()
        {
            var temp = _dbContext.ShopOrders.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate).ToList();
            return View(temp);
        }

    }
}
