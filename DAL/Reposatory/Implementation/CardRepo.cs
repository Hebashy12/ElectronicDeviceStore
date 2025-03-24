using DAL.Database;
using DAL.Entities;
using DAL.Reposatory.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Reposatory.Implementation
{
    public class CardRepo : ICardRepo
    {
        private readonly ApplicationDbContext _context;

        public CardRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddToCart(Card item)
        {
            var existingItem = _context.Cards.FirstOrDefault(c => c.ProductId == item.ProductId && c.UserId == item.UserId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                _context.Cards.Update(existingItem);
            }
            else
            {
                _context.Cards.Add(item);
            }

            _context.SaveChanges();
        }

        public void RemoveFromCart(int productId, string userId)
        {
            var item = _context.Cards.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
            if (item != null)
            {
                _context.Cards.Remove(item);
                _context.SaveChanges();
            }
        }

        public List<Card> GetCartItems(string userId)
        {
            var x= _context.Cards.Where(c => c.UserId == userId).ToList();
            return _context.Cards.Where(c => c.UserId == userId).ToList();
        }

        public int GetCartCount(string userId)
        {
            return _context.Cards.Where(c => c.UserId == userId).Sum(c => c.Quantity);
        }

        public void UpdateCartItem(Card item)
        {
            _context.Cards.Update(item);
            _context.SaveChanges();
        }
    }
}
