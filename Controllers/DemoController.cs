using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly MyDbContext _context;
        public DemoController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<List<Products>> GetListProduct(int maSP)
        {
            List<Products> pro = new List<Products>();
            var products = _context.Products.ToList();
            return products;

        }
    }
}
