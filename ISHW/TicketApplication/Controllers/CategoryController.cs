using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketApplication.Data.Repository.IRepository;
using TicketApplication.Models;
using TicketApplication.Utility;

namespace TicketApplication.Areas.Admin.Controllers
{

    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> cateroryList = _unitOfWork.CategoryRepository.GetAll().ToList();
            return View(cateroryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order can not match the name!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Add(obj);
                _unitOfWork.Save();
                TempData["sucess"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }


            Category? category = _unitOfWork.CategoryRepository.Get(x => x.Id == id);
            

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order can not match the name!");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Update(obj);
                _unitOfWork.Save();
                TempData["sucess"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View();

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.CategoryRepository.Get(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? category = _unitOfWork.CategoryRepository.Get(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.CategoryRepository.Remove(category);
            _unitOfWork.Save();
            TempData["sucess"] = "Category deleted successfully!";
            return RedirectToAction("Index");


        }

    }
}
