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
    public class MoviesRepository : IRepository<Movie>
    {
        private ApplicationDbContext _context;
        internal DbSet<Movie> dbSet;

        public MoviesRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<Movie>();
        }

        public void Add(Movie entity)
        {
            dbSet.Add(entity);
        }

        public Movie Get(Expression<Func<Movie, bool>> filter)
        {
            IQueryable<Movie> query = dbSet.Include(x => x.Category);
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<Movie> GetAll()
        {
            IQueryable<Movie> query = dbSet.Include(x => x.Category);
            return query.ToList();
        }

        public void Remove(Movie entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Movie> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Movie entity)
        {
            _context.Update(entity);
        }
    }
}
