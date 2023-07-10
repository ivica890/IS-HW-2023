using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketApplication.Data.Data;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models.Models;

namespace TicketApplication.Data.Repository.Imp
{
    public class ShoppingCartRepository : IRepository<ShoppingCart>
    {

        private readonly ApplicationDbContext _context;
        internal DbSet<ShoppingCart> _dbSet;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<ShoppingCart>();
        }

        public void Add(ShoppingCart entity)
        {
            _dbSet.Add(entity);
        }

        public ShoppingCart Get(Expression<Func<ShoppingCart, bool>> filter)
        {
            IQueryable<ShoppingCart> query = _dbSet.Include(x => x.User)
                .Include(x => x.showingsInShoppingCarts)
                .Include("showingsInShoppingCarts.MovieShowing")
                .Include("showingsInShoppingCarts.MovieShowing.Movie")
                .Include("showingsInShoppingCarts.MovieShowing.CinemaHall");
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<ShoppingCart> GetAll()
        {
            IQueryable<ShoppingCart> query = _dbSet.Include(x => x.User);
            return query.ToList();
        }

        public void Remove(ShoppingCart entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<ShoppingCart> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ShoppingCart entity)
        {
            _context.Update(entity);
        }
    }
}
