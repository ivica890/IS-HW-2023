using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketApplication.Models.Models;

namespace TicketApplication.Services.Interface
{
    public interface IShoppingCartService
    {
        IEnumerable<ShoppingCart> GetAll();
        ShoppingCart Get(Expression<Func<ShoppingCart, bool>> filter);
        void Add(ShoppingCart entity);

        void AddMovieShowingToShoppingCart(string userId,MovieShowing movieShowing, int number_of_tickets);

        //void Update(T entity);
        void Remove(ShoppingCart entity);

        void RemoveMovieShowingFromShoppingCart(string userId, MovieShowing movieShowing);

        void RemoveRange(IEnumerable<ShoppingCart> entities);

        void Update(ShoppingCart entity);

        void Save();

    }
}
