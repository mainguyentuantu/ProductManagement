using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    public class OrderController : Controller
    {
        private readonly MyDbContext _context;

        public OrderController(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.Include(o => o.Product).ToListAsync();
            return View(orders);
        }
        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["Products"] =_context.Products.ToList();
            return View();
        }

        public IActionResult ProductIndex()
        {
            //get all the initial data from the repository
            var productlist = _context.Products;

            //set select items for the DropDownList
            ViewBag.Types = _context.Products.Select(c => c.TenSP).Distinct().ToList().Select(c => new SelectListItem { Text = c, Value = c }).ToList();
            return View(productlist);
        }
        //based on the type filter data and return partial view.
        public IActionResult ShowProduct(string type)
        {
            var productlist = _context.Products.ToList();

            //based on the type to filter data.
            if (!string.IsNullOrEmpty(type) && type != "all")
            {
                productlist = productlist.Where(c => c.TenSP == type).ToList();
            }

            //required using Microsoft.AspNetCore.Mvc.Rendering;
            ViewBag.Types = _context.Products.Select(c => new SelectListItem { Text = c.TenSP, Value = c.TenSP }).Distinct().ToList();
            return PartialView("_ShowProductPartialView", productlist);
        }
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.MaDH == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        // POST: Order/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
      
        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDH,MaSP,TenKhachHang,SoLuongDH,ThanhTien")] Order order)
        {
            if (id != order.MaDH)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.MaDH))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        [HttpGet("GetProductPrice")] // Giả sử một hành động bộ điều khiển để lấy giá
        public IActionResult GetProductPrice(int maSP)
        {
            var product = _context.Products.FirstOrDefault(p => p.MaSP == maSP);
            if (product != null)
            {
                return Ok(new { GiaCa = product.GiaCa }); // Trả về giá dưới dạng đối tượng JSON
            }
            else
            {
                return NotFound(); // Xử lý trường hợp không tìm thấy sản phẩm
            }
        }


        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.MaDH == id);
        }
    } 
    
}

