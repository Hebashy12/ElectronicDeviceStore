

using BLL.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Stripe.Checkout;
using Microsoft.AspNetCore.Http.Headers;
using PL.ActionResults;
using BLL.DTOs;
using BLL.Services.Absraction;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DAL.Entities;

namespace PL.Controllers
{
    public class PaymentController : Controller
    {
        
        private readonly ICardServices _cardServices;
        private readonly IProductServices _productServices;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public PaymentController(ICardServices cardServices, IProductServices productServices, IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _cardServices = cardServices;
            _productServices = productServices;
            _orderService = orderService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Checkout(int id, int Quantity)
        {
            //Claim IdClaims = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            //var order = _cardServices.GetCartItems(IdClaims.Value.ToString());
            var order = new List<ProductDTO>();
            var product = await _productServices.GetbyId(id);
            order.Add(product);
            
            
            var domain = "https://localhost:7216/";
            
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"Payment/OrderConfiermation",
                CancelUrl = domain + $"Account/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode ="payment"
            };
            foreach (var item in order)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * Quantity),
                        Currency = "USD",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name
                        }
                    },
                    Quantity = Quantity
                };
                options.LineItems.Add(sessionLineItem);

            }
            var services = new SessionService();
            Session session = services.Create(options);
            Response.Headers.Add("Location", session.Url);
            Claim IdClaims = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var userModel = await _userManager.FindByIdAsync(IdClaims.Value);
            await _orderService.PlaceOrder(userModel.Id.ToString(),id,product.Price*Quantity);
            return new StatusCodeResult(303);
        }
        public IActionResult OrderConfiermation()
        {
            return View("Index");
        }
    }
}
