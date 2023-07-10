using System.Linq.Expressions;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models.Models;
using TicketApplication.Services.Interface;

namespace TicketApplication.Services.Impl
{
    public class MovieShowingService : IMovieShowingService
    {
        private readonly IRepository<MovieShowing> repository;

        public MovieShowingService(IRepository<MovieShowing> _repository)
        {
            repository = _repository;
        }

        public void Add(MovieShowing entity, int capacity)
        {
            entity.AvailableSeats = capacity;

            repository.Add(entity);
        }

        public MovieShowing Get(Expression<Func<MovieShowing, bool>> filter)
        {
            return repository.Get(filter);
        }

        public IEnumerable<MovieShowing> GetAll()
        {
            return repository.GetAll();
        }

        public void Remove(MovieShowing entity)
        {
            repository.Remove(entity);
        }

        public void RemoveRange(IEnumerable<MovieShowing> entities)
        {
            repository.RemoveRange(entities);
        }

        public void Save()
        {
            repository.Save();
        }

        public void Update(MovieShowing entity)
        {
            repository.Update(entity);
        }
    }
}
