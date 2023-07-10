using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketApplication.Models.Models;

namespace TicketApplication.Services.Interface
{
    public interface IMovieShowingService
    {
        IEnumerable<MovieShowing> GetAll();
        MovieShowing Get(Expression<Func<MovieShowing, bool>> filter);
        void Add(MovieShowing entity, int capacity);
        //void Update(T entity);
        void Remove(MovieShowing entity);
        void RemoveRange(IEnumerable<MovieShowing> entities);

        void Update(MovieShowing entity);

        void Save();

    }
}
