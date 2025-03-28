using PL.VMs;

namespace PL.ConvertIntoVM
{
    public class UserVM
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<OrderVM>? orders { get; set; }
        //public string Role { get; set; }
    }
}
