using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDienThoai.Models;
using WebDienThoai.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace WebDienThoai.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseTheKingContext _context;

        public AccountController(DatabaseTheKingContext context)
        {
            _context = context;
        }

        // --- ĐĂNG NHẬP ---
        [HttpGet]
        public IActionResult Login()
        {
            // Nếu đã đăng nhập (có Session Role) thì đá về trang chủ
            if (HttpContext.Session.GetString("Role") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View("Login", model);

            string passHash = HashPassword(model.Password);
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.Passwordhash == passHash);

            if (user != null)
            {
                if (user.Role == "PendingAdmin")
                {
                    ModelState.AddModelError("", "Tài khoản đang chờ duyệt.");
                    return View("Login", model);
                }

                // --- LƯU SESSION---
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("FullName", user.Fullname ?? "Khách hàng");
                HttpContext.Session.SetString("Role", user.Role ?? "Customer"); 

                // Điều hướng
                if (user.Role == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            return View("Login", model);
        }

        // --- ĐĂNG XUẤT ---
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa sạch Session
            return RedirectToAction("Index", "Home");
        }

        // --- ĐĂNG KÝ  ---
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại.");
                    return View(model);
                }

                var user = new User
                {
                    Username = model.Username,
                    Fullname = model.FullName,
                    Email = model.Email,
                    Passwordhash = HashPassword(model.Password),
                    Role = (model.SelectedRole == "Admin") ? "PendingAdmin" : "Customer"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Đăng ký thành công! Mời bạn đăng nhập.";
                return RedirectToAction("Login");
            }
            return View(model);
        }
        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }

        private string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return string.Empty;
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}