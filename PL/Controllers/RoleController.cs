using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.ActionResults;

namespace PL.Controllers
{
    [Authorize(Roles ="Admin")]
    
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> New(RoleAR role)
        {
            if (ModelState.IsValid)
            {
                var roleModel = new IdentityRole() { Name = role.RoleName };
                IdentityResult result = await _roleManager.CreateAsync(roleModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(role);
        }
    }
}
