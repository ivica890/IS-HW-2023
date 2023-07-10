using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TicketApplication.Data.Repository.Imp;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models.Models;

namespace TicketApplication.Controllers
{
    public class CinemaHallController : Controller
    {

        private readonly IRepository<CinemaHall> _repository;
        public CinemaHallController(IRepository<CinemaHall> repository)
        {
            _repository = repository;   
        }

        public IActionResult Index()
        {
            List<CinemaHall> cinemaHalls = _repository.GetAll().ToList(); 
            return View(cinemaHalls);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CinemaHall obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.Add(obj);
                _repository.Save();
                TempData["sucess"] = "Cinema Hall was added!";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? Id)
        {

          
            CinemaHall? ToEdit = _repository.Get(x => x.Id == Id);
            if (ToEdit == null)
            {
                return NotFound();
            }
            return View(ToEdit);
        }

        [HttpPost]
        public IActionResult Edit(CinemaHall obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
         
                _repository.Update(obj);
                _repository.Save();
                TempData["sucess"] = "Cinema Hall was updated!";
                return RedirectToAction("Index");
            }
            return View(obj.Id);
        }

        public IActionResult Delete(int? Id)
        {

            CinemaHall? ToDelete = _repository.Get(x => x.Id == Id);
            if (ToDelete == null)
            {
                return NotFound();
            }
            return View(ToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(CinemaHall obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            _repository.Remove(obj);
            _repository.Save();
            TempData["sucess"] = "Cinema Hall was removed!";
            return RedirectToAction("Index");

        }


    }
}
