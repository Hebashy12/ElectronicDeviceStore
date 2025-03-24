using DAL.Entities;

namespace PL.ActionResults
{
    public class ProductAR
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
