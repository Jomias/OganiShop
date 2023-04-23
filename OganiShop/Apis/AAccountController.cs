using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OganiShop.Entities;
using OganiShop.Models;
using System;
using System.Security.Claims;

namespace OganiShop.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AAccountController : ControllerBase
    {
        private readonly OganiShopContext _dbContext;
        public AAccountController(OganiShopContext dbContext, IWebHostEnvironment enviroment)
        {
            _dbContext = dbContext;
        }

    }
}
