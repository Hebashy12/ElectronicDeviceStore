using DAL.Database;
using DAL.Entities;
using DAL.Reposatory.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Reposatory.Implementation
{
    public class OrderRepository : IOrderRepo
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void PlaceOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public List<Order> GetOrdersByUser(string userId)
        {
            return _context.Orders
                           .Where(o => o.UserId == userId)
                           .Include(o => o.OrderItems)
                           .ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders
                           .Include(o => o.OrderItems)
                           .FirstOrDefault(o => o.Id == orderId);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        
    }

}
