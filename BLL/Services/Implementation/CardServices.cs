using BLL.DTOs;
using BLL.Services.Abstraction;
using DAL.Entities;
using DAL.Reposatory.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services.Implementation
{
    public class CardServices : ICardServices
    {
        private readonly ICardRepo _cardRepo;
        private readonly IProductRepo _productRepo;

        public CardServices(ICardRepo cardRepo,IProductRepo productRepo)
        {
            _cardRepo = cardRepo;
            _productRepo = productRepo;
        }

        public async Task AddToCart(CardDTO item)
        {
            var cardEntity = new Card
            {
                ProductId = item.ProductId,
                UserId = item.UserId,
                Quantity = item.Quantity,
                Product = await _productRepo.GetbyId(item.ProductId)
            };

            _cardRepo.AddToCart(cardEntity);
        }

        public void RemoveFromCart(int productId, string userId)
        {
            _cardRepo.RemoveFromCart(productId, userId);
        }

        public async Task<List<CardDTO>>  GetCartItems(string userId)
        {
            
            var r = _cardRepo.GetCartItems(userId);
            var p = await _productRepo.GetbyId(r[0].ProductId);
            var x = r.Select(c => new CardDTO
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                UserId = c.UserId,
                Product = new ProductDTO()
                ,

                //// Fetch product details separately
                ProductName = p.Name,
                ImageUrl = p.ImageUrl,
                Price = p.Price
            }).ToList();
            return _cardRepo.GetCartItems(userId).Select(c => new CardDTO
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                UserId = c.UserId,
                Product = new ProductDTO()
                ,

                //// Fetch product details separately
                ProductName = c.Product.Name,
                ImageUrl = c.Product.ImageUrl,
                Price = c.Product.Price
            }).ToList();
        }


        public int GetCartCount(string userId)
        {
            return _cardRepo.GetCartCount(userId);
        }

        public void UpdateCartItem(CardDTO item)
        {
            var cardEntity = new Card
            {
                ProductId = item.ProductId,
                UserId = item.UserId,
                Quantity = item.Quantity
            };

            _cardRepo.UpdateCartItem(cardEntity);
        }

       
    }
}
