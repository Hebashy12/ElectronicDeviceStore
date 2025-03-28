using BLL.DTOs;
using BLL.Services.Absraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PL.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        //public async Task<IActionResult> Buy(int id)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        //return RedirectToAction("Login", "Account");
        //        userId = "1";
        //    }

        //    await _orderService.PlaceOrder(userId);
        //    return RedirectToAction("Index");
        //}

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                //return RedirectToAction("Login", "Account");
                userId = "1";
            }

            var orders = _orderService.GetOrdersByUser(userId);
            return View(orders);
        }

        public IActionResult Edit(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();

            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(OrderDTO order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            _orderService.UpdateOrder(order);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();

            _orderService.DeleteOrder(id);
            return RedirectToAction("Index");
        }
    }
}
