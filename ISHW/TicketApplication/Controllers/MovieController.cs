using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models.Models;
using TicketApplication.Models.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace TicketApplication.Areas.Customer.Controllers
{
    public class MovieController : Controller
    {
        private IRepository<Movie> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieController(IRepository<Movie> repository, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }

        public IActionResult Index()
        {
            List<Movie> movies = _repository.GetAll().ToList();
          
            return View(movies);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.CategoryList = CategoryList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie obj, IFormFile? file)
        {
            if(obj == null) {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\movie");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImageUrl = @"\images\movie\" + fileName;
                }

                _repository.Add(obj);
                _repository.Save();
                TempData["sucess"] = "Movie was added!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? Id)
        {

            IEnumerable<SelectListItem> CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.CategoryList = CategoryList;

            Movie? ToEdit = _repository.Get(x => x.Id == Id);
            if (ToEdit == null)
            {
                return NotFound();
            }
            return View(ToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Movie obj, IFormFile? file)
        {
            if (obj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\movie");

                    if(!string.IsNullOrEmpty(obj.ImageUrl)) 
                    { 
                        
                        var oldImagePath = Path.Combine(wwwRootPath,obj.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImageUrl = @"\images\movie\" + fileName;
                }

                _repository.Update(obj);
                _repository.Save();
                TempData["sucess"] = "Movie was updated!";
                return RedirectToAction("Index");
            }
            return View(obj.Id);
        }

        public IActionResult Delete(int? Id)
        {

            Movie? ToDelete = _repository.Get(x => x.Id == Id);
            if (ToDelete == null)
            {
                return NotFound();
            }
            return View(ToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Movie obj)
        {
            if (obj == null)
            {
                return NotFound();
            }
            _repository.Remove(obj);
            _repository.Save();
            TempData["sucess"] = "Movie was removed!";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Movie> movies = _repository.GetAll().ToList();
            return Json(new { data = movies});
        }


        
        public IActionResult DeleteApi(int? id) {
            var toDelete = _repository.Get(x => id == x.Id);

            if(toDelete == null)
            {
                return NotFound();
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                toDelete.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _repository.Remove(toDelete);
            _repository.Save();

            List<Movie> movies = _repository.GetAll().ToList();
            return Json(new { sucess = "true", message = "Delete successful!" });
        }
    }
}
