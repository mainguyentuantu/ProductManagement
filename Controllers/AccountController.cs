using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

public class AccountController : Controller
{
	private readonly MyDbContext _context;

	public AccountController(MyDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public IActionResult Login()
	{
		return PartialView(); // Trả về view Login
	}

	// POST: Accounts/SignUp
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> SignUp([Bind("UserName,Email,Password")] Accounts accounts)
	{
		if (ModelState.IsValid)
		{
			_context.Add(accounts);
			await _context.SaveChangesAsync();

			// Lưu thông báo vào TempData
			TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";

			return RedirectToAction(nameof(Login));
		}
		return View(accounts);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> SignIn(string email, string password)
	{
		// Kiểm tra xác thực tài khoản với email và mật khẩu
		var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
		if (account != null)
		{
			// Đăng nhập thành công, thực hiện các thao tác cần thiết như lưu thông tin đăng nhập vào session và chuyển hướng
			// ở đây tôi chỉ chuyển hướng đến trang chính là "Index" của "Home" Controller
			return RedirectToAction("Index", "Home");
		}

		// Đăng nhập thất bại, hiển thị lại trang đăng nhập với thông báo lỗi
		ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
		return View();
	}


}
