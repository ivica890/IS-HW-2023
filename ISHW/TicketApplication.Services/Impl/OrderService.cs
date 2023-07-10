using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketApplication.Data.Repository.Imp;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models.Models;
using TicketApplication.Models.Relationship;
using TicketApplication.Services.Interface;

namespace TicketApplication.Services.Impl
{
    public class OrderService : IOrderService
    {

        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<MovieShowing> _movieShowingRepository;
        private readonly IRepository<ShowingInOrder> _showingInOrderRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;



        public OrderService(IRepository<Order> orderRepository, IRepository<MovieShowing> movieShowingRepository,
            IRepository<ShowingInOrder> showingInOrderRepository, IRepository<ShoppingCart> shoppinCartRepository
            )
        {
            _orderRepository = orderRepository;
            _movieShowingRepository = movieShowingRepository;
            _showingInOrderRepository = showingInOrderRepository;
            _shoppingCartRepository = shoppinCartRepository;
        }

        public void Add(Order entity)
        {
            _orderRepository.Add(entity);
        }

        public void AddCartToOrder(string userId, ShoppingCart cart)
        {
            throw new NotImplementedException();
        }

        public bool CompleteOrder(string userId)
        {
          

            ShoppingCart? cart = _shoppingCartRepository.Get(x => x.UserId == userId);


            

            Order order = new Order
            {
                userId = userId,
                totalSum = cart.totalSum,
                orderDate = DateTime.Now,
                showingsInOrder = new List<ShowingInOrder> ()
            };

            _orderRepository.Add(order);

            foreach (ShowingInShoppingCart obj in cart.showingsInShoppingCarts) {

                ShowingInOrder showing = new ShowingInOrder
                {
                    OrderId = order.Id,
                    MovieShowingId = obj.MovieShowingId,
                };

                obj.MovieShowing.AvailableSeats -= obj.Quantity;
                _movieShowingRepository.Update(obj.MovieShowing);

                order.showingsInOrder.Add(showing);
            }
            _showingInOrderRepository.Save();


            _orderRepository.Update(order);


            _shoppingCartRepository.Remove(cart);



            _shoppingCartRepository.Save();
            _movieShowingRepository.Save();
            _shoppingCartRepository.Save();

            return true;
 

        }

        public Order Get(Expression<Func<Order, bool>> filter)
        {
            return _orderRepository.Get(filter);
        }

        public IEnumerable<Order> GetAllForUser(string userId)
        {
            return GetAll().Where(x => x.userId == userId);
        }
        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public void Remove(Order entity)
        {
            _orderRepository.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Order> entities)
        {
            _orderRepository.RemoveRange(entities);
        }

        public void Save()
        {
            _orderRepository.Save();
        }

        public void Update(Order entity)
        {
            _orderRepository.Update(entity);
        }

        public int GetTotalSumForUser(string userId)
        {
            List<Order> ordersForUser = GetAllForUser(userId).ToList();

            int totalSum = 0;

            foreach(Order order in ordersForUser)
            {
                totalSum += order.totalSum;
            }

            return totalSum;
        }
    }
}
