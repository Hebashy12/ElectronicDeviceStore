using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Absraction
{
    public interface ICategoryServices
    {
        Task<CategoryDTO> GetbyId(int id);
        Task<List<CategoryDTO>> GetAll();
        Task CreateNew(CategoryDTO category);
        Task EditCategory(int id, CategoryDTO newCategory);
        Task Delete(int id);
    }
}
