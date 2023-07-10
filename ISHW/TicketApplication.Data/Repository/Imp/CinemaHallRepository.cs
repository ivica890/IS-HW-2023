using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
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
    public class CinemaHallRepository : IRepository<CinemaHall>
    {

        private readonly ApplicationDbContext _context;
        internal DbSet<CinemaHall> _dbSet;

        public CinemaHallRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<CinemaHall>();
        }


        public void Add(CinemaHall entity)
        {
            _dbSet.Add(entity);
        }

        public CinemaHall Get(Expression<Func<CinemaHall, bool>> filter)
        {
            IQueryable<CinemaHall> query = _dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<CinemaHall> GetAll()
        {
            IQueryable<CinemaHall> query = _dbSet;
            return query.ToList();
        }

        public void Remove(CinemaHall entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<CinemaHall> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Save()
        {
            
            _context.SaveChanges();
        }

        public void Update(CinemaHall entity)
        {
            _context.Update(entity);
        }
    }
}
