using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OganiShop.Entities;
using OganiShop.Helpers;

namespace OganiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly OganiShopContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "UserImage";
        public UserController(OganiShopContext dbContext, IMapper mapper, IFileStorageService fileStorageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
