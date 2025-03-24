using BLL.DTOs;
using BLL.Services.Absraction;
using DAL.Entities;
using DAL.Reposatory.Abstraction;
using DAL.Reposatory.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementation
{
    public class CategoryServices:ICategoryServices
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryServices(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task CreateNew(CategoryDTO category)
        {
            var categoryEntity = new Category
            {
                Products = category.Products,
               
                Description = category.Description,
                Id = category.Id,
                Name = category.Name,
            };
            await _categoryRepo.CreateNew(categoryEntity);
        }

        public async Task Delete(int id)
        {
            await _categoryRepo.Delete(id);
        }

        public async Task EditCategory(int id, CategoryDTO newCategory)
        {
            var CategoryEntity = new Category
            {
                Products = newCategory.Products,
                
                Description = newCategory.Description,
                Id = newCategory.Id,
                Name = newCategory.Name,
            };
            await _categoryRepo.EditCategory(id, CategoryEntity);
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            var listEntity = await _categoryRepo.GetAll();
            var listDTO = listEntity.Select(c => new CategoryDTO
            {
                Products = c?.Products ?? default,
                Name = c?.Name ?? "",
                
                Description = c?.Description ?? default,
                Id = c?.Id ?? default,
                
            }).ToList();
            return listDTO;
        }

        public async Task<CategoryDTO> GetbyId(int id)
        {
            var categoryEntity = await _categoryRepo.GetbyId(id);
            var categoryDTO = new CategoryDTO()
            {
                
                Description = categoryEntity.Description,
                Id = categoryEntity.Id,
                Name = categoryEntity.Name,
                Products = categoryEntity.Products
            };
            return categoryDTO;
        }
    }
}
