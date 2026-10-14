using Microsoft.AspNetCore.Mvc;
using Shop.DAL;
using Shop.Services;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopContext _db;
        private readonly ShopService _shopService;

        public HomeController(ShopContext db, ShopService shopService)
        {
            _db = db;
            _shopService = shopService;
        }

        // Главная страница — список товаров
        public IActionResult Index()
        {
            var products = _db.Products.ToList();
            return View(products);
        }

        // Покупка: POST с productId и count
        [HttpPost]
        public IActionResult Buy(int productId, int count)
        {
            _shopService.BuyProduct(productId, count);
            return RedirectToAction("Index");
        }
    }
}
