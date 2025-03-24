using BLL.DTOs;
using BLL.Services.Absraction;
using Microsoft.AspNetCore.Mvc;
using PL.ActionResults;
using PL.ConvertIntoVM;

namespace PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _categoryServices;
        private readonly IWebHostEnvironment _webHost;

        public ProductController(IProductServices productServices, ICategoryServices categoryServices, IWebHostEnvironment webHost)
        {
            _productServices = productServices;
            _categoryServices = categoryServices;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productServices.GetAll();
            var result = products.FromListDTOToVM();
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.category = await _categoryServices.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductAR newProduct)
        {
            var fileName = string.Empty;
            if (newProduct.Image != null && newProduct.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "_" + newProduct.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStreem = new FileStream(filePath, FileMode.Create))
                {
                    newProduct.Image.CopyTo(fileStreem);
                }

            }



            ViewBag.category = await _categoryServices.GetAll();
            if (ModelState.IsValid)
            {
                ProductDTO product = new ProductDTO
                {
                    Id = newProduct.Id,
                    Price = newProduct.Price,
                    Description = newProduct.Description,
                    Name = newProduct.Name,
                    Category = newProduct.Category,
                    CategoryId = newProduct.CategoryId,
                    ImageUrl = fileName
                };
                await _productServices.CreateNew(product);
                return RedirectToAction("Index");
            }
            return View(newProduct);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _productServices.GetbyId(id);
            var AR = new ProductAR
            {
                Name = dto.Name,
                Description = dto.Description,
                Category = dto.Category,
                CategoryId = dto.CategoryId,
                
                Price = dto.Price
            };
            return View(AR);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductAR productAR)
        {
            var old = await _productServices.GetbyId(id);
            var fileName = old.ImageUrl;
            if (productAR.Image != null && productAR.Image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "_" + productAR.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStreem = new FileStream(filePath, FileMode.Create))
                {
                    productAR.Image.CopyTo(fileStreem);
                }
                
                
            }
            if (ModelState.IsValid)
            {
                await _productServices.EditProduct(id, new ProductDTO
                {
                    Name = productAR.Name,
                    Description = productAR.Description,
                    Category = productAR.Category,
                    CategoryId = productAR.CategoryId,
                    ImageUrl = fileName,
                    Price = productAR.Price
                });
                return RedirectToAction("Index");
            }
            return View(productAR);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productServices.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productServices.GetbyId(id);
            return View(product);
        }

        public async Task<IActionResult> ProductPage(int id)
        {
            var product = await _productServices.GetbyId(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
