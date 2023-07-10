using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketApplication.Data.Data;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models.Models;

namespace TicketApplication.Data.Repository.Imp
{
    public class OrderRepository : IRepository<Order>
    {
        private ApplicationDbContext _context;
        internal DbSet<Order> dbSet;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<Order>();
        }

        public void Add(Order entity)
        {
            dbSet.Add(entity);
        }

        public Order Get(Expression<Func<Order, bool>> filter)
        {
            IQueryable<Order> query = dbSet.Include(x => x.applicationUser)
                .Include(x => x.showingsInOrder)
                .Include("showingsInOrder.MovieShowing")
                .Include("showingsInOrder.MovieShowing.Movie")
                .Include("showingsInOrder.MovieShowing.CinemaHall");
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<Order> GetAll()
        {
            IQueryable<Order> query = dbSet.Include(x => x.applicationUser)
                .Include(x => x.showingsInOrder)
                .Include("showingsInOrder.MovieShowing")
                .Include("showingsInOrder.MovieShowing.Movie")
                .Include("showingsInOrder.MovieShowing.CinemaHall");
            return query.ToList();
        }

        public void Remove(Order entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Order> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Order entity)
        {
            _context.Update(entity);
        }
    }
}
