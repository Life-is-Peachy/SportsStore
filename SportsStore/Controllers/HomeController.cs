using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;
        private ICategoryRepository _catRepository;

        public HomeController(IRepository repo, ICategoryRepository crepo)
        {
            _repository = repo;
            _catRepository = crepo;
        }

        public IActionResult Index()
            => View(_repository.Products);
        #region old
        [HttpPost]
        //public IActionResult AddProduct(Product product)
        //{
        //    repository.AddProduct(product);
        //    return RedirectToAction(nameof(Index));
        //}

        //// Отправляет продукт на форму для редактирования
        //public IActionResult UpdateProduct(long key)
        //    => View(repository.GetProduct(key));

        //[HttpPost]
        //// Сохраняет изменения продукта
        //public IActionResult UpdateProduct(Product product)
        //{
        //    repository.UpdateProduct(product);
        //    return RedirectToAction(nameof(Index));
        //}

        //public IActionResult UpdateAll()
        //{
        //    ViewBag.UpdateAll = true;
        //    return View(nameof(Index), repository.Products);
        //}

        //[HttpPost]
        //public IActionResult UpdateAll(Product[] products)
        //{
        //    repository.UpdateAll(products);
        //    return RedirectToAction(nameof(Index));
        //}
        #endregion

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
        public IActionResult DeleteProduct(Product product)
        {
            _repository.DeleteProduct(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
