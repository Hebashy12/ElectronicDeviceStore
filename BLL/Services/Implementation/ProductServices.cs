using BLL.DTOs;
using BLL.Services.Absraction;
using DAL.Entities;
using DAL.Reposatory.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementation
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepo _productRepo;

        public ProductServices(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public async Task CreateNew(ProductDTO product)
        {
            var productEntity = new Product 
                                { Category = product.Category, CategoryId = product.CategoryId, Description = product.Description,
                                         Id = product.Id, ImageUrl = product.ImageUrl, Name = product.Name, Price = product.Price };
            await _productRepo.CreateNew(productEntity);
        }

        public async Task Delete(int id)
        {
           await _productRepo.Delete(id);
        }

        public async Task EditProduct(int id, ProductDTO newProduct)
        {
            var productEntity = new Product
            {
                Category = newProduct.Category,
                CategoryId = newProduct.CategoryId,
                Description = newProduct.Description,
                Id = newProduct.Id,
                ImageUrl = newProduct.ImageUrl,
                Name = newProduct.Name,
                Price = newProduct.Price
            };
            await _productRepo.EditProduct(id, productEntity);
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            var listEntity= await _productRepo.GetAll();
            var listDTO = listEntity.Select(p => new ProductDTO 
                            { Price = p?.Price ?? 0, Name = p?.Name??"", Category = p?.Category?? default, CategoryId = p?.CategoryId??default,
                                Description = p?.Description??default, Id = p?.Id??default, ImageUrl = p?.ImageUrl??default }).ToList();
            return listDTO;
        }

        public async Task<ProductDTO> GetbyId(int id)
        {
            var productEntity =await _productRepo.GetbyId(id);
            var productDTO = new ProductDTO() 
                        { ImageUrl = productEntity.ImageUrl, Category = productEntity.Category, CategoryId = productEntity.CategoryId,
                            Description = productEntity.Description,Id = productEntity.Id, 
                            Name = productEntity.Name, Price = productEntity.Price };
            return productDTO;
        }
    }
}
