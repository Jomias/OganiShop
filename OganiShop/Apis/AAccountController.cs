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

        [HttpGet("GetAccount")]
        public IActionResult GetAccount()
        {
            var claims = HttpContext.User.Identity as ClaimsIdentity;
            if (claims.FindFirst(ClaimTypes.NameIdentifier) == null)
            {
                return Ok("");
            }
            var account = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _dbContext.Users.FirstOrDefault(x => x.Account == account);
            string username = user.FirstName + " " + user.LastName;
            return Ok(username);
        }
    }
}
