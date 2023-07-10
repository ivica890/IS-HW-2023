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
    public class MovieShowingRepository : IRepository<MovieShowing>
    {

        private ApplicationDbContext _context;
        internal DbSet<MovieShowing> dbSet;

        public MovieShowingRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<MovieShowing>();
        }


        public void Add(MovieShowing entity)
        {
            dbSet.Add(entity);
        }

        public MovieShowing Get(Expression<Func<MovieShowing, bool>> filter)
        {
            IQueryable<MovieShowing> query = dbSet.Include(x => x.Movie)
                .Include(x=> x.Movie.Category)
                .Include(x => x.CinemaHall);
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<MovieShowing> GetAll()
        {
            IQueryable<MovieShowing> query = dbSet.Include(x => x.Movie)
                .Include(x => x.CinemaHall);
            return query.ToList();
        }

        public void Remove(MovieShowing entity)
        {
            dbSet.Remove(entity);   
        }

        public void RemoveRange(IEnumerable<MovieShowing> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(MovieShowing entity)
        {
            _context.Update(entity);
        }
    }
}
