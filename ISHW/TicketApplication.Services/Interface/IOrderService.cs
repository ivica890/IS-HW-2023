using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketApplication.Models.Models;

namespace TicketApplication.Services.Interface
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        Order Get(Expression<Func<Order, bool>> filter);
        void Add(Order entity);

        void AddCartToOrder(string userId, ShoppingCart cart);
        IEnumerable<Order> GetAllForUser(string userId);

        int GetTotalSumForUser(string userId);

        bool CompleteOrder(string userId);

        //void Update(T entity);
        void Remove(Order entity);
        void RemoveRange(IEnumerable<Order> entities);

        void Update(Order entity);

        void Save();
    }
}
