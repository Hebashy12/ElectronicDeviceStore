using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Reposatory.Abstraction
{
    public interface ICardRepo
    {
        void AddToCart(Card item);
        void RemoveFromCart(int productId, string userId);  // ✅ Ensure it includes userId
        List<Card> GetCartItems(string userId); // ✅ Ensure it includes userId
        int GetCartCount(string userId); // ✅ Ensure it includes userId
        void UpdateCartItem(Card item);
    }
}
