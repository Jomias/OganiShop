using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OganiShop.Entities;
using OganiShop.Helpers;
using OganiShop.Models;
using OganiShop.Utils;
using System.Linq;
using System.Security.Claims;

namespace OganiShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ProductImageController : Controller
    {
        private readonly OganiShopContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "product";
        public ProductImageController(OganiShopContext dbContext, IMapper mapper, IFileStorageService fileStorageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }
        public IActionResult Index(int Id = 0)
        {
            if (_dbContext.Products.FirstOrDefault(x => x.Id == Id && x.IsDeleted == false) == null)
            {
                return BadRequest();
            }
            ViewBag.Id = Id;
            var temp =_dbContext.ProductImages.Where(x => x.IsDeleted == false && x.ProductId == Id).ToList();
            return View(temp);
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
        public IActionResult AddOrEdit(int Id = 0, int ProductId = 0)
        {
            ViewBag.Id = Id;
            ViewBag.ProductId = ProductId;
            var temp = _dbContext.ProductImages.Find(Id);
            if (temp == null)
            {
                var temp1 = new ProductImageModel();
                if (ProductId != 0) 
                {
                    temp1.ProductId = ProductId;
                }
                return View(temp1);
            }
            return View(_mapper.Map<ProductImageModel>(temp));
        }
        public IActionResult Delete(int Id)
        {

            var entity = _dbContext.ProductImages.Find(Id);
            if (entity == null) 
            {
                TempData["Message"] = "Can't Remove";
                return RedirectToAction("Index", new { Id });
            }
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = GetAccount();
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            TempData["Message"] = "Remove ProductImage Successfully";
            return RedirectToAction("Index", new { Id = entity.ProductId });
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEdit(ProductImageModel model, IFormFile? ImageFile)
        {
            ViewBag.Id = model.Id == null ? 0 : model.Id;
            if (ImageFile != null) 
            {
                model.Name = ImageFile.FileName;
                if (model.Path == null)
                {

                    model.Path = await _fileStorageService.SaveFile(containerName, ImageFile);
                }
                else
                {
                    model.Path = await _fileStorageService.EditFile(containerName, ImageFile, model.Path);
                }
                ModelState["Path"].ValidationState = ModelValidationState.Valid;
                ModelState["Path"].RawValue = model.Path;
                ModelState["Name"].ValidationState = ModelValidationState.Valid;
                ModelState["Name"].RawValue = model.Name;
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var temp = _mapper.Map<ProductImage>(model);
            if (model.Id == null)
            {

                if (!_dbContext.ProductImages.Where(x => x.IsDeleted == false && x.IsAvatar == true && x.ProductId == temp.ProductId).Any())
                {
                    temp.IsAvatar = true;
                    var product = _dbContext.Products.Find(model.ProductId);
                    product.Image = temp.Path;
                    product.UpdatedBy = GetAccount();
                    product.UpdatedDate = DateTime.Now;
                    _dbContext.Update(product);
                    _dbContext.SaveChanges();
                }
                _dbContext.Add(temp);
                _dbContext.SaveChanges();

                TempData["Message"] = "Add ProductImage Successfully";
                return RedirectToAction("Index", new { Id = model.ProductId});
            }
            var account = GetAccount();

            temp.UpdatedDate = DateTime.Now;
            temp.UpdatedBy = account;
            _dbContext.Update(temp);
            _dbContext.SaveChanges();
            TempData["Message"] = "Edit ProductImage Successfully";
            return RedirectToAction("Index", new { Id = model.ProductId });

        }

        public IActionResult SetAvatar(int Id)
        {
            var temp = _dbContext.ProductImages.Find(Id);
            var z = temp.ProductId;
            var query = _dbContext.ProductImages.FirstOrDefault(x => x.ProductId == temp.ProductId && x.IsAvatar == true);
            temp.IsAvatar = true;
            temp.UpdatedBy = GetAccount();
            temp.UpdatedDate = DateTime.Now;
            query.UpdatedBy = GetAccount();
            query.UpdatedDate = DateTime.Now;
            query.IsAvatar = false;
            _dbContext.Update(temp);
            _dbContext.SaveChanges();
            TempData["Message"] = "Set ProductImage to Avatar Successfully";
            _dbContext.Update(query);
            _dbContext.SaveChanges();
            var product = _dbContext.Products.Find(z);
            product.Image = temp.Path;
            product.UpdatedBy = GetAccount();
            product.UpdatedDate = DateTime.Now;
            _dbContext.Update(product);

            _dbContext.SaveChanges();
            return Redirect($"/Admin/ProductImage?Id={z}");
        }
    }
}
