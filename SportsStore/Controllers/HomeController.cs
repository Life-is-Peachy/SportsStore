using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{

    public class HomeController : Controller
    {
        private IRepository _repository;
        private ICategoryRepository _catRepository;

        public HomeController(IRepository repo, ICategoryRepository catRepo)
        {
            _repository = repo;
            _catRepository = catRepo;
        }

        public IActionResult Index()
            => View(_repository.Products);

        public IActionResult UpdateProduct(long key)
        {
            ViewBag.Categories = _catRepository.Categories;
            return View(key == 0 ? new Product() : _repository.GetProduct(key));
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (product.Id == 0)
                _repository.AddProduct(product);
            else
                _repository.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _repository.DeleteProduct(product);
            return RedirectToAction(nameof(Index));
        }
    }
}