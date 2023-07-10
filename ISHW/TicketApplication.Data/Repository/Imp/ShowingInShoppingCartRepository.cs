using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketApplication.Data.Data;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models.Models;
using TicketApplication.Models.Relationship;

namespace TicketApplication.Data.Repository.Imp
{
    public class ShowingInShoppingCartRepository : IRepository<ShowingInShoppingCart>
    {

            private readonly ApplicationDbContext _context;
            internal DbSet<ShowingInShoppingCart> _dbSet;

            public ShowingInShoppingCartRepository(ApplicationDbContext context)
            {
                _context = context;
                _dbSet = _context.Set<ShowingInShoppingCart>();
            }

            public void Add(ShowingInShoppingCart entity)
            {
                _dbSet.Add(entity);
            }

            public ShowingInShoppingCart Get(Expression<Func<ShowingInShoppingCart, bool>> filter)
            {
                IQueryable<ShowingInShoppingCart> query = _dbSet.Include(x => x.MovieShowing);
                query = query.Where(filter);
                return query.FirstOrDefault();
            }

            public IEnumerable<ShowingInShoppingCart> GetAll()
            {
                IQueryable<ShowingInShoppingCart> query = _dbSet.Include(x => x.MovieShowing);
                return query.ToList();
            }

            public void Remove(ShowingInShoppingCart entity)
            {
                _dbSet.Remove(entity);
            }

            public void RemoveRange(IEnumerable<ShowingInShoppingCart> entities)
            {
                _dbSet.RemoveRange(entities);
            }

            public void Save()
            {
                _context.SaveChanges();
            }

            public void Update(ShowingInShoppingCart entity)
            {
                _context.Update(entity);
            }

        ShowingInShoppingCart IRepository<ShowingInShoppingCart>.Get(Expression<Func<ShowingInShoppingCart, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
