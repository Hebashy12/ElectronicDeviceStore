using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<Product> OrderItems { get; set; } = new List<Product>();
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
