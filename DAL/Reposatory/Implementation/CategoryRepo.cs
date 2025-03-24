using DAL.Database;
using DAL.Entities;
using DAL.Reposatory.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatory.Implementation
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateNew(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category =await _context.Categories.FirstAsync(c => c.Id == id);
            if(category!= default)
            {
                 _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

            }
            
        }

        public async Task EditCategory(int id, Category newCategory)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null && newCategory != null)
            {
                category.Name = newCategory.Name;
                category.Description = newCategory.Description;
                category.Products = newCategory.Products;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetbyId(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
