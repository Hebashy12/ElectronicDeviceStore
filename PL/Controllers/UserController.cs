using BLL.Services.Absraction;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PL.ActionResults;
using PL.ConvertIntoVM;
using PL.VMs;
using System.Security.Claims;

namespace PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        private readonly IProductServices _productServices;

        public UserController(UserManager<ApplicationUser> userManager, IOrderService orderService, IProductServices productServices)
        {
            _userManager = userManager;
            _orderService = orderService;
            _productServices = productServices;
        }
        public async Task<IActionResult> Detailes()
        {
            Claim IdClaims = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            //Claim RoleClaims = User.Claims.FirstOrDefault(c => c);
            var userModel = await _userManager.FindByIdAsync(IdClaims.Value);
            if (userModel != null)
            {
                var order = _orderService.GetOrdersByUser(userModel.Id.ToString());
                var orderList = new List<OrderVM>();
                foreach (var item in order)
                {
                    var product = await _productServices.GetbyId(item.ProductId);
                    orderList.Add(new OrderVM { ProductName = product.Name, OrderDate = item.OrderDate, TotalAmount = item.TotalAmount });
                }
                
                UserVM user = new UserVM() { UserName = userModel.UserName, Email = userModel.Email, PhoneNumber = userModel.PhoneNumber , orders = orderList };
                
                //ViewBag.order = order;
                return View(user);
            }
            return RedirectToAction("account","login");
        }
    }
}
