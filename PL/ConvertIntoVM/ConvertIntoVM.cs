using BLL.DTOs;
using PL.ActionResults;
using PL.VMs;

namespace PL.ConvertIntoVM
{
    public static class ConvertIntoVM
    {
       
        public static IEnumerable<ProductVM> FromListDTOToVM(this List<ProductDTO> dTO)
        {
            if (dTO.Where(p => p.Id != default).ToArray().Length > 0)
            {
                List<ProductVM> product = dTO.Select(p => new ProductVM
                {
                    Id = p.Id,
                    Price = p.Price,
                    Description = p.Description,
                    Name = p.Name,
                    Category = p.Category,
                    CategoryId = p.CategoryId,
                    ImageUrl = p.ImageUrl
                }).ToList();
                return product;
            }
            return default;
           
        }
        public static ProductDTO FromARToDTO(this ProductAR p)
        {

                 ProductDTO product =  new ProductDTO
                 {
                    Id = p.Id,
                    Price = p.Price,
                    Description = p.Description,
                    Name = p.Name,
                    Category = p.Category,
                    CategoryId = p.CategoryId,
                    //ImageUrl = p.ImageUrl
                };
                return product;
           
            

        }

    }
}
