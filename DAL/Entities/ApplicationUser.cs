using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
