using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatory.Abstraction
{
    public interface ICategoryRepo
    {
        Task<Category> GetbyId(int id);
        Task<List<Category>> GetAll();
        Task CreateNew(Category category);
        Task EditCategory(int id, Category newCategory);
        Task Delete(int id);
    }
}
