using System.ComponentModel.DataAnnotations;

namespace PL.ActionResults
{
    public class RegisterationUserAR
    {
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string CanfarmPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        
    }
}
