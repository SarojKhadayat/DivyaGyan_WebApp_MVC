using DivyaGyan_WebApp_MVC.Models;
using DivyaGyan_WebApp_MVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DivyaGyan_WebApp_MVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var products = new ProductRepository().GetProducts();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            var productRepository=new ProductRepository();
            productRepository.CreateProduct(product);
            return RedirectToAction("Index");
        }
    }
}
