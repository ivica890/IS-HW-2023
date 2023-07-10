using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketApplication.Data.Data;
using TicketApplication.Data.Repository.IRepository;
namespace TicketApplication.Data.Repository.Imp
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public ICategoryRepository CategoryRepository { get; set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
       
    }
}
