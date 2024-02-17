using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            //phan trang
            ViewData["CurrentSort"] = sortOrder;
            //sort
            ViewData["TenSPSortParm"] = String.IsNullOrEmpty(sortOrder) ? "TenSP_desc" : "TenSP";
            ViewData["KichThuocSortParm"] = String.IsNullOrEmpty(sortOrder) ? "KichThuoc" : "";
            ViewData["ChatLieuSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ChatLieu" : "";
            ViewData["MauSacSortParm"] = String.IsNullOrEmpty(sortOrder) ? "MauSac" : "";
            ViewData["KieuDangSortParm"] = String.IsNullOrEmpty(sortOrder) ? "KieuDang" : "";
            ViewData["ThuongHieuSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ThuongHieu" : "";
            ViewData["GiaCaSortParm"] = String.IsNullOrEmpty(sortOrder) ? "GiaCa" : "";
            ViewData["SoLuongTongSortParm"] = String.IsNullOrEmpty(sortOrder) ? "SoLuongTong" : "";


            ViewData["CurrentFilter"] = searchString; //search

            //phan trang
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var products = from s in _context.Products
                           select s;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.TenSP.Contains(searchString));
            }

                switch (sortOrder)
                {
                    case "TenSP":
                        products = products.OrderBy(s => s.TenSP);
                        break;

                    case "KichThuoc":
                        products = products.OrderBy(s => s.KichThuoc);
                        break;

                    case "ChatLieu":
                        products = products.OrderBy(s => s.ChatLieu);
                        break;

                    case "MauSac":
                        products = products.OrderBy(s => s.MauSac);
                        break;

                    case "KieuDang":
                        products = products.OrderBy(s => s.KieuDang);
                        break;

                    case "ThuongHieu":
                        products = products.OrderBy(s => s.ThuongHieu);
                        break;

                    case "GiaCa":
                        products = products.OrderBy(s => s.GiaCa);
                        break;
                    case "SoLuongTong":
                    products = products.OrderBy(s => s.SoLuongTong);
                    break;

                default:
                    products = products.OrderByDescending(s => s.TenSP);
                    break;

            }

            int pageSize = 3;
            return View(await PaginatedList<Products>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.MaSP == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSP,TenSP,KichThuoc,ChatLieu,MauSac,KieuDang,ThuongHieu,GiaCa,SoLuongTong")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSP,TenSP,KichThuoc,ChatLieu,MauSac,KieuDang,ThuongHieu,GiaCa,SoLuongTong")] Products products)
        {
            if (id != products.MaSP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.MaSP))
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
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.MaSP == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Order/5
        public async Task<IActionResult> Order(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.ProductName = product.TenSP;
            ViewBag.ProductPrice = product.GiaCa;

            return View();
        }

        // POST: Products/Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Order(Order order)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FindAsync(order.ProductId);
                if (product == null)
                {
                    return NotFound();
                }

                var newOrder = new Order
                {
                    ProductId = order.ProductId,
                    TenKhachHang = order.TenKhachHang,
                    SoLuongDatHang = order.SoLuongDatHang,
                    TongTien = order.SoLuongDatHang * product.GiaCa
                };

                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }


        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.MaSP == id);
        }
    }
}
