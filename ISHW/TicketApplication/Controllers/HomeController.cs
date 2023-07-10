using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models;
using TicketApplication.Models.Models;
using TicketApplication.Services.Interface;

namespace TicketApplication.Areas.Customer.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Movie> _movieRepository;
        private readonly IMovieShowingService _movieShowingService;
        public HomeController(ILogger<HomeController> logger, IRepository<Movie> repository, IMovieShowingService movieShowingService)
        {
            _logger = logger;
            _movieRepository = repository;
            _movieShowingService = movieShowingService;
        }

        public IActionResult Index()
        {
            IEnumerable<MovieShowing> movieShowings = _movieShowingService.GetAll();
            return View(movieShowings);
        }

        public IActionResult Details(int? id)
        {
            MovieShowing movieShowing = _movieShowingService.Get(x => x.Id == id);
            return View(movieShowing);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}