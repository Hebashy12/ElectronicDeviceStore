using BLL.DTOs;
using BLL.Services.Absraction;
using DAL.Entities;
using DAL.Reposatory.Abstraction;
using DAL.Reposatory.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICardRepo _cardRepo;
        private readonly IProductRepo _productRepo;

        public OrderService(IOrderRepo orderRepo, ICardRepo cardRepo, IProductRepo productRepo)
        {
            _orderRepo = orderRepo;
            _cardRepo = cardRepo;
            _productRepo = productRepo;
        }

        public async Task PlaceOrder(string userId, int productId, decimal totalAmount)
        {
            //var cartItems = _cardRepo.GetCartItems(userId).ToList();
            //if (!cartItems.Any()) return;

            var order = new Order
            {
                ProductId=productId,
                UserId = userId,
                TotalAmount = totalAmount,
                OrderItems = new List<Product> { await _productRepo.GetbyId(productId) },
                OrderDate = DateTime.Now
            };

            _orderRepo.PlaceOrder(order);
        }

        public List<OrderDTO> GetOrdersByUser(string userId)
        {
            var orders = _orderRepo.GetOrdersByUser(userId);

            return orders.Select(o => new OrderDTO
            {
                ProductId = o.ProductId,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                OrderItems = o.OrderItems.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                }).ToList()
            }).ToList();
        }

        public OrderDTO GetOrderById(int orderId)
        {
            var order = _orderRepo.GetOrderById(orderId);
            if (order == null) return null;

            return new OrderDTO
            {
                ProductId = order.ProductId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                }).ToList()
            };
        }

        public void UpdateOrder(OrderDTO orderDTO)
        {
            var order = _orderRepo.GetOrderById(orderDTO.ProductId);
            if (order != null)
            {
                order.TotalAmount = orderDTO.TotalAmount;
                order.OrderItems = orderDTO.OrderItems.Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                }).ToList();
                _orderRepo.UpdateOrder(order);
            }
        }

        public void DeleteOrder(int orderId)
        {
            _orderRepo.DeleteOrder(orderId);
        }
    }

}
