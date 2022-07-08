using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class CartRepository : BaseRepository
    {
        public List<Cart> GetCartItems(string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Carts.Include(c => c.Book).Where(c => keyword == null || c.Book.Name.ToLower().Contains(keyword)).AsQueryable();
            return query.ToList();
        }
        public BaseList<CartResponse> GetAll(int UserId)
        {

            var query = _context.Carts.AsQueryable();
            BaseList<CartResponse> result = new BaseList<CartResponse>();
            List<CartResponse> getCartModels = new List<CartResponse>();


            query = query.Where(cart => (cart.Userid == UserId));

            foreach (Cart cart in query.ToList())
            {
                CartResponse getCartModel = new CartResponse();
                getCartModel.Id = cart.Id;
                getCartModel.UserId = cart.Userid;

                Book book = _context.Books.Where(b => b.Id == cart.Bookid).FirstOrDefault();
                BookModel bookModel = new BookModel(book);
                getCartModel.Book = bookModel;
                getCartModel.Quantity = cart.Quantity;
                getCartModels.Add(getCartModel);
            }

            result.TotalRecords = getCartModels.Count();
            result.Records = getCartModels;
            return result;

        }
        public ListResponse<Cart> GetCartListall(int Userid)
        {

            var query = _context.Carts.Include(c => c.Book).Where(c => c.Userid == Userid).AsQueryable();

            int totalReocrds = query.Count();
            List<Cart> carts = query.ToList();

            return new ListResponse<Cart>()
            {
                Results = carts,
                TotalRecords = totalReocrds,
            };
        }


        public Cart GetCarts(int id)
        {
            return _context.Carts.FirstOrDefault(c => c.Id == id);
        }

        public Cart AddCart(Cart category)
        {
            var entry = _context.Carts.Add(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Cart UpdateCart(Cart category)
        {
            var entry = _context.Carts.Update(category);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCart(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
                return false;

            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return true;
        }
    }
}
