using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatory.Abstraction
{
    public interface IOrderRepo
    {
        void PlaceOrder(Order order);
        List<Order> GetOrdersByUser(string userId);
        Order GetOrderById(int orderId);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
    }
}
