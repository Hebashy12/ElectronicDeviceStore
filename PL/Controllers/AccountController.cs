using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.ActionResults;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signin(RegisterationUserAR userAccount )
        {
            
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser() { UserName = userAccount.UserName, Email = userAccount.Email, PasswordHash = userAccount.Password, PhoneNumber = userAccount.PhoneNumber };
                IdentityResult result =await _userManager.CreateAsync(userModel,userAccount.Password);
                if (result.Succeeded)
                {
                    if (userModel.UserName == "Admin")
                    {
                        await _userManager.AddToRoleAsync(userModel,"Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(userModel, userAccount.Role);
                    }
                       
                    await _signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("Password", item.Description);
                    }
                   
                }
            }
                    return View(userAccount);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginAR login)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel= await _userManager.FindByNameAsync(login.UserName);
                if (userModel != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(userModel, login.Password);
                    if (found)
                    {
                        await _signInManager.SignInAsync(userModel, login.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "UserName or Password Wrong");
            }
            return View(login);
        }
    }
}
