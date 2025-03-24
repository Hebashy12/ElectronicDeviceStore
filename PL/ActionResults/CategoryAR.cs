using DAL.Entities;

namespace PL.ActionResults
{
    public class CategoryAR
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product>? Products { get; set; } = new List<Product>();
    }
}
