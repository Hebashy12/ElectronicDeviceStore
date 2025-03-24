using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Absraction
{
    public interface IOrderService
    {
        void PlaceOrder(string userId);
        List<OrderDTO> GetOrdersByUser(string userId);
        OrderDTO GetOrderById(int orderId);
        void UpdateOrder(OrderDTO orderDTO);
        void DeleteOrder(int orderId);
    }
}
