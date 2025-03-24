using BLL.Services.Absraction;
using BLL.Services.Implementation;
using Microsoft.AspNetCore.Mvc;
using PL.ActionResults;
using PL.VMs;

namespace PL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetAll();
            var categoryVM = categories.Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Products = c.Products
            });
            return View(categoryVM);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryAR newCategory)
        {
            if (ModelState.IsValid)
            {
                await _categoryServices.CreateNew(new BLL.DTOs.CategoryDTO 
                       { Id =newCategory.Id, Name = newCategory.Name, Description = newCategory.Description, Products = newCategory.Products});
                return RedirectToAction("Index");

            }

            return View(newCategory);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _categoryServices.GetbyId(id);
            var AR = new CategoryAR { Name = dto.Name, Description = dto.Description, Products = dto.Products };
            return View(AR);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryAR categoryAR)
        {
            if (ModelState.IsValid)
            {
               await _categoryServices.EditCategory(id, new BLL.DTOs.CategoryDTO
                { Name = categoryAR.Name, Description = categoryAR.Description, Products = categoryAR.Products });
                return RedirectToAction("Index");
            }
            return View(categoryAR);
        }
        public async Task<IActionResult> Delete(int id)
        {
           await _categoryServices.Delete(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _categoryServices.GetbyId(id));
        }
    }
}
