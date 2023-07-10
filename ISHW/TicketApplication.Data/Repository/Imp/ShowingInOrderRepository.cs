using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketApplication.Data.Data;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models.Relationship;

namespace TicketApplication.Data.Repository.Imp
{
    public class ShowingInOrderRepository : IRepository<ShowingInOrder>
    {

        private readonly ApplicationDbContext _context;
        internal DbSet<ShowingInOrder> _dbSet;

        public ShowingInOrderRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<ShowingInOrder>();
        }

        public void Add(ShowingInOrder entity)
        {
            _dbSet.Add(entity);
        }

        public ShowingInOrder Get(Expression<Func<ShowingInOrder, bool>> filter)
        {
            IQueryable<ShowingInOrder> query = _dbSet.Include(x => x.MovieShowing)
                .Include(x => x.MovieShowing.Movie)
                .Include(x => x.MovieShowing.CinemaHall);
            query = query.Where(filter);
            return query.FirstOrDefault();
        }


        public IEnumerable<ShowingInOrder> GetAll()
        {
            IQueryable<ShowingInOrder> query = _dbSet.Include(x => x.MovieShowing)
                .Include(x => x.MovieShowing.Movie)
                .Include(x => x.MovieShowing.CinemaHall);
            return query.ToList();
        }

        public void Remove(ShowingInOrder entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<ShowingInOrder> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ShowingInOrder entity)
        {
            _context.Update(entity);
        }

        ShowingInOrder IRepository<ShowingInOrder>.Get(Expression<Func<ShowingInOrder, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
