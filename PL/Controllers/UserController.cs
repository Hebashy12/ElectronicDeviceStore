using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.ActionResults;
using PL.ConvertIntoVM;
using System.Security.Claims;

namespace PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Detailes()
        {
            Claim IdClaims = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            //Claim RoleClaims = User.Claims.FirstOrDefault(c => c);
            var userModel = await _userManager.FindByIdAsync(IdClaims.Value);
            if (userModel != null)
            {
                UserVM user = new UserVM() { UserName = userModel.UserName, Email = userModel.Email, PhoneNumber = userModel.PhoneNumber };
                return View(user);
            }
            return RedirectToAction("account","login");
        }
    }
}
