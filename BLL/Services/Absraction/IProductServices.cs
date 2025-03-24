using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Absraction
{
    public interface IProductServices
    {
        Task<ProductDTO> GetbyId(int id);
        Task<List<ProductDTO>> GetAll();
        Task CreateNew(ProductDTO product);
        Task EditProduct(int id, ProductDTO newProduct);
        Task Delete(int id);
    }
}
