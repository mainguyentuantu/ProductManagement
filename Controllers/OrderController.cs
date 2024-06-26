﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ProductManagement.Models;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost("CreateConfirmed")]
        public async Task<IActionResult> CreateConfirmed(Order order)
        {
            if (ModelState.IsValid)
            {
                // Tự động thiết lập thời gian tạo đơn hàng mới
                order.ThoiGianTao = DateTime.Now;

                // Thêm đơn hàng mới vào cơ sở dữ liệu
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Cập nhật số lượng tồn của sản phẩm sau khi tạo đơn hàng
                var product = await _context.Products.FindAsync(order.MaSP);
                product.SoLuongTong -= order.SoLuongDH; // Trừ số lượng đặt hàng khỏi số lượng tổng
                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                // Chuyển hướng người dùng đến trang Index để xem danh sách đơn hàng
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Nếu dữ liệu không hợp lệ, hiển thị lại trang Create để người dùng nhập lại
                ViewData["Products"] = _context.Products.ToList();
                return View("Create", order);
            }
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

            // Load thông tin về sản phẩm
            var products = await _context.Products.ToListAsync();

            ViewData["Products"] = new SelectList(products, "MaSP", "TenSP", order.MaSP);

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDH,TenKhachHang,MaSP,SoLuongDH,ThanhTien,ThoiGianTao")] Order order)
        {
            if (id != order.MaDH)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Truy xuất thông tin sản phẩm từ cơ sở dữ liệu bằng MaSP
                var product = await _context.Products.FindAsync(order.MaSP);

                try
                {
                    // Cập nhật thời gian chỉnh sửa
                    order.ThoiGianTao = DateTime.Now;

                    // Lấy số lượng tổng mới
                    var newTotalQuantity = product.SoLuongTong - (order.SoLuongDH - (await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.MaDH == id)).SoLuongDH);

                    // Cập nhật số lượng tổng sản phẩm trong cơ sở dữ liệu
                    product.SoLuongTong = newTotalQuantity;

                    // Cập nhật thông tin đơn hàng trong cơ sở dữ liệu
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
                // Chuyển hướng người dùng đến trang Index để xem danh sách đơn hàng sau khi chỉnh sửa
                return RedirectToAction(nameof(Index));
            }
            // Nếu dữ liệu không hợp lệ, hiển thị lại view Edit để người dùng nhập lại
            ViewData["Products"] = _context.Products.ToList();
            return View(order);
        }

        [HttpPost]
        // POST: Order/UpdateStock
        public IActionResult UpdateStock(int productId, int newStock)
        {
            // Tìm sản phẩm theo productId trong cơ sở dữ liệu
            var product = _context.Products.Find(productId);

            if (product != null)
            {
                // Cập nhật số lượng tồn mới
                product.SoLuongTong = newStock;
                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                return Ok(); // Trả về kết quả thành công
            }
            else
            {
                return NotFound(); // Trả về mã lỗi 404 nếu không tìm thấy sản phẩm
            }
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.MaDH == id);
        }
		[HttpGet("test")]
		public async Task<List<Products>> GetListProduct(int maSP)
		{
			List<Products> pro = new List<Products>();
			var products = _context.Products.ToList();
			return products;

		}
	} 
    
}

