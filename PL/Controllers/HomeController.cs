using System.Diagnostics;
using BLL.Services.Absraction;
using Microsoft.AspNetCore.Mvc;
using PL.Models;
using PL.VMs;

namespace PL.Controllers;

public class HomeController : Controller
{
        private readonly ICategoryServices _categoryServices;
        private readonly IProductServices _productServices;

        public HomeController(ICategoryServices categoryServices, IProductServices productServices)
        {
            _categoryServices = categoryServices;
            _productServices = productServices;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetAll();
            var homeViewModel = new HomeViewModel
            {
                Categories = categories
            };

            foreach (var category in categories)
            {
                var products = (await _productServices.GetAll())
                               .Where(p => p.CategoryId == category.Id)
                               .Take(5)
                               .ToList();

                homeViewModel.ProductsByCategory[category.Id] = products;
            }

            return View(homeViewModel);
        }
        

}
