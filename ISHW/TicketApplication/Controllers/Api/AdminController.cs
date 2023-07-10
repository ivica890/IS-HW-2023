using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models.Models;

namespace TicketApplication.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRepository<Movie> _moviesRepository;
        private readonly IRepository<Order> _orderRepository;

        public AdminController(IRepository<Movie> moviesRepository, IRepository<Order> orderRepository)
        {
            _moviesRepository = moviesRepository;
            _orderRepository = orderRepository;

        }

        [HttpGet("get-all-movies")]
        public List<Movie> GetAllMovies()
        {
            return _moviesRepository.GetAll().ToList();
        }


        [HttpGet("get-all-orders")]
        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll().ToList();
        }
    }
}
