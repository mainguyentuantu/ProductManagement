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
            ViewData["Products"] = _context.Products.ToList();
            return View();
        }

        // GET: Order/CreateConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed(Order order)
        {
            var product = await _context.Products.FindAsync(order.MaSP);
            if (product == null)
            {
                ModelState.AddModelError("MaSP", "Sản phẩm không tồn tại");
                return View(order);// Hiển thị lại view với thông báo lỗi

            }
            else
            {
                
                // Cập nhật giá trị dựa trên sản phẩm đã chọn
                order.ThanhTien = order.SoLuongDH * product.GiaCa;

                _context.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));// Chuyển hướng đến view danh sách đơn hàng
            }

        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Orders
                .FirstOrDefaultAsync(m => m.MaDH == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }
        // POST: Order/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Orders.FindAsync(id);
            if (products != null)
            {
                _context.Orders.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.MaDH == id);
        }
    } 
    
}

