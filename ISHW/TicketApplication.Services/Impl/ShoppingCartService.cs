using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketApplication.Data.Repository.Imp;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models.Models;
using TicketApplication.Models.Relationship;
using TicketApplication.Services.Interface;

namespace TicketApplication.Services.Impl
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<ShowingInShoppingCart> _showingInShoppingCartRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<ShowingInShoppingCart>  showingInShoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _showingInShoppingCartRepository = showingInShoppingCartRepository;
        }

        public void Add(ShoppingCart entity)
        {
            _shoppingCartRepository.Add(entity);
        }

        public void AddMovieShowingToShoppingCart(string userId,MovieShowing movieShowing, int number_of_tickets)
        {
        
            ShoppingCart? cart = _shoppingCartRepository.Get(x => x.UserId == userId);
            
            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    totalSum = 0
                };

                Add(cart);
                _shoppingCartRepository.Save();
            }

            ShowingInShoppingCart showingInShoppingCart = new ShowingInShoppingCart
            {
                ShoppingCartId = cart.Id,
                MovieShowingId = movieShowing.Id,
                Quantity = number_of_tickets
     

            };

            _showingInShoppingCartRepository.Add(showingInShoppingCart);
            _showingInShoppingCartRepository.Save();
            cart.showingsInShoppingCarts.Add(showingInShoppingCart);

            UpdateTotalSum(cart);

            _shoppingCartRepository.Update(cart);
            _shoppingCartRepository.Save();

        }

        public void UpdateTotalSum(ShoppingCart cart)
        {
            double totalSum = 0;
            foreach(var obj in cart.showingsInShoppingCarts)
            {
                totalSum += obj.Quantity * obj.MovieShowing.Movie.TicketPrice;
            }
            cart.totalSum = (int)totalSum;
            _shoppingCartRepository.Update(cart);
            _shoppingCartRepository.Save();
        }

        public ShoppingCart Get(Expression<Func<ShoppingCart, bool>> filter)
        {
            return _shoppingCartRepository.Get(filter);
        }

        public IEnumerable<ShoppingCart> GetAll()
        {
            return _shoppingCartRepository.GetAll();
        }

        public void Remove(ShoppingCart entity)
        {
            _shoppingCartRepository.Remove(entity);
        }

        public void RemoveMovieShowingFromShoppingCart(string userId, MovieShowing movieShowing)
        {
            ShoppingCart cart = _shoppingCartRepository.Get(x => x.UserId == userId);

            ShowingInShoppingCart showing =  _showingInShoppingCartRepository.Get(x => x.MovieShowingId == movieShowing.Id);

            cart.showingsInShoppingCarts.Remove(showing);

            _showingInShoppingCartRepository.Remove(showing);
            _shoppingCartRepository.Update(cart);
            _shoppingCartRepository.Save();
            _showingInShoppingCartRepository.Save();
        }

        public void RemoveRange(IEnumerable<ShoppingCart> entities)
        {
            _shoppingCartRepository.RemoveRange(entities);
        }

        public void Save()
        {
            _shoppingCartRepository.Save();
        }

        public void Update(ShoppingCart entity)
        {
            _shoppingCartRepository.Update(entity);
        }
    }
}
