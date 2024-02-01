using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using System.Diagnostics;

namespace ProductManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //private readonly MyDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //_dbContext = dbContext;
        }

        public IActionResult Index()
        {
           
/*            ProductModel model = new ProductModel();
            model.ProductList = new List<Products>();
            var data = _dbContext.Products.ToList();*/
            return View();
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
