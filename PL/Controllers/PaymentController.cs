

using BLL.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Stripe.Checkout;
using Microsoft.AspNetCore.Http.Headers;
using PL.ActionResults;
using BLL.DTOs;
using BLL.Services.Absraction;
using System.Threading.Tasks;

namespace PL.Controllers
{
    public class PaymentController : Controller
    {
        
        private readonly ICardServices _cardServices;
        private readonly IProductServices _productServices;

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public PaymentController(ICardServices cardServices, IProductServices productServices)
        {
           _cardServices = cardServices;
            _productServices = productServices;
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
            return new StatusCodeResult(303);
        }
        public IActionResult OrderConfiermation()
        {
            return View();
        }
    }
}
