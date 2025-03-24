using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatory.Abstraction
{
    public interface IProductRepo
    {
        Task<Product> GetbyId(int id);
        Task<List<Product>> GetAll();
        Task CreateNew(Product product);
        Task EditProduct(int id , Product newProduct);
        Task Delete(int id);
    }
}
