using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDienThoai.Models;
using WebDienThoai.ViewModels;

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
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChuyenTrang(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

          
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.Passwordhash == model.Password);

            if (user != null)
            {
           
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("FullName", user.Fullname ?? "User"); 
                HttpContext.Session.SetString("Role", user.Role ?? "Customer");

                if (user.Role == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
            }

            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            return View("Login", model);
        }

        // --- ĐĂNG XUẤT ---
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // --- ĐĂNG KÝ (Thêm mới để tạo user vào DB) ---
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
                
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username || u.Email == model.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc Email đã tồn tại.");
                    return View(model);
                }

                // Tạo user mới
                var newUser = new User
                {
                    Username = model.Username,
                    Fullname = model.FullName,
                    Email = model.Email,
                    Passwordhash = model.Password, 
                    Role = "Customer" 
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}