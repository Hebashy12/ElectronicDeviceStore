using BLL.DTOs;
using BLL.Services.Absraction;
using BLL.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PL.Controllers
{
    public class CartController : Controller
    {
        private readonly ICardServices _cardServices;
        private readonly IProductServices _productServices;

        public CartController(ICardServices cardServices,IProductServices productServices)
        {
            _cardServices = cardServices;
            _productServices = productServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int Id, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                //return Unauthorized();
                userId = "1";
            }
            var product = await _productServices.GetbyId(Id);
            var cartItemDto = new CardDTO
            {
                ProductId = Id,
                Quantity = quantity,
                UserId = userId,
                Product=product
                
            };

          await _cardServices.AddToCart(cartItemDto);

            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                //return Unauthorized();
                userId = "1";
            }

            var cartItems = await _cardServices.GetCartItems(userId);
            return View(cartItems);
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                //return Unauthorized();
                userId = "1";
            }

            _cardServices.RemoveFromCart(productId, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                //return Unauthorized();
                userId = "1";
            }

            var updatedItem = new CardDTO
            {
                ProductId = productId,
                Quantity = quantity,
                UserId = userId
            };

            _cardServices.UpdateCartItem(updatedItem);
            return RedirectToAction("Index");
        }
    }
}
