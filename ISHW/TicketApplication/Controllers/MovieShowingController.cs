using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models.Models;
using TicketApplication.Services.Interface;

namespace TicketApplication.Controllers
{
    public class MovieShowingController : Controller
    {
        private readonly IMovieShowingService _movieShowingService;
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<CinemaHall> _cinemaHallRepository;

        public MovieShowingController(IMovieShowingService movieShowingRepository,IRepository<Movie> movieRepository, IRepository<CinemaHall> cinemaRepository)
        {
            _movieShowingService = movieShowingRepository;
            _movieRepository = movieRepository;
            _cinemaHallRepository = cinemaRepository;
        }


        public IActionResult Index()
        {
            List<MovieShowing> movies = _movieShowingService.GetAll().ToList();

            return View(movies);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> movieList = _movieRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            IEnumerable<SelectListItem> hallsList = _cinemaHallRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.movieList = movieList;
            ViewBag.hallsList = hallsList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(MovieShowing obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                int cinemaHallCapacity = _cinemaHallRepository.Get(x => x.Id == obj.CinemaHallId).Capacity;
                _movieShowingService.Add(obj,cinemaHallCapacity);
                _movieShowingService.Save();
                TempData["sucess"] = "Movie showing was added!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? Id)
        {

            IEnumerable<SelectListItem> movieList = _movieRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            IEnumerable<SelectListItem> hallsList = _cinemaHallRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.movieList = movieList;
            ViewBag.hallsList = hallsList;

            MovieShowing? ToEdit = _movieShowingService.Get(x => x.Id == Id);
            if (ToEdit == null)
            {
                return NotFound();
            }
            return View(ToEdit);
        }

        [HttpPost]
        public IActionResult Edit(MovieShowing obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                _movieShowingService.Update(obj);
                _movieShowingService.Save();
                TempData["sucess"] = "Movie showing was updated!";
                return RedirectToAction("Index");
            }
            return View(obj.Id);
        }

        public IActionResult Delete(int? Id)
        {

            MovieShowing? ToDelete = _movieShowingService.Get(x => x.Id == Id);
            if (ToDelete == null)
            {
                return NotFound();
            }
            return View(ToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(MovieShowing obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            _movieShowingService.Remove(obj);
            _movieShowingService.Save();
            TempData["sucess"] = "Movie showing was removed!";
            return RedirectToAction("Index");

        }


        public IActionResult AddToCard(int id)
        { 
            return View();
        }
    }
}
