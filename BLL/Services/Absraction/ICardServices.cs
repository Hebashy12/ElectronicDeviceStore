using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstraction
{
    public interface ICardServices
    {
      Task AddToCart(CardDTO item);
        void RemoveFromCart(int productId, string userId);
        Task<List<CardDTO> > GetCartItems(string userId);
        int GetCartCount(string userId);
        void UpdateCartItem(CardDTO item);
    }
}