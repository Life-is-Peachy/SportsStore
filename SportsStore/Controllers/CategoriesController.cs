using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repo)
            => _repository = repo;

        public IActionResult Index()
            => View(_repository.Categories);

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _repository.AddCategory(category);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditCategory(long id)
        {
            ViewBag.EditId = id;
            return View("Index", _repository.Categories);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _repository.UpdateCategory(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteCategory(Category category)
        {
            _repository.DeleteCategory(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
