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
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateNew(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product =await _context.Products.FirstOrDefaultAsync(p=>p.Id==id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

            }

        }

        public async Task EditProduct(int id, Product newProduct)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product!=null && newProduct != null)
            {
                product.Price = newProduct.Price;
                product.Name = newProduct.Name;
                product.Description = newProduct.Description;
                product.Category = newProduct.Category;
                product.CategoryId = newProduct.CategoryId;
                product.ImageUrl = newProduct.ImageUrl;
                await _context.SaveChangesAsync();
            }
           
            
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products?.ToListAsync();
        }

        public async Task<Product> GetbyId(int id)
        {
            var x = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
