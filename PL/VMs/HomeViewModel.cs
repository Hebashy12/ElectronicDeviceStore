using BLL.DTOs;
using DAL.Entities;

namespace PL.VMs
{
    public class HomeViewModel
    {
            public List<CategoryDTO> Categories { get; set; } = new();
            public Dictionary<int, List<ProductDTO>> ProductsByCategory { get; set; } = new();
            
            public int CartItemCount { get; set; } // 🛒

    }
}
