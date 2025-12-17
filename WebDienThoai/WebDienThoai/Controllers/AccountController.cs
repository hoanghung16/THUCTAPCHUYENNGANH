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
            string passHash = HashPassword(model.Password);
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.Passwordhash == passHash);
            if (user != null)
            {               
                if (user.Role == "PendingAdmin")
                {
                    ModelState.AddModelError("", "Tài khoản Admin này đang chờ duyệt, vui lòng quay lại sau.");
                    return View("Login", model);
                }             
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
                    
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã được sử dụng.");
                }

                
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email này đã được đăng ký bởi tài khoản khác.");
                }

                
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

               
                var user = new User
                {
                    Username = model.Username,
                    Fullname = model.FullName,
                    Email = model.Email,
                    Passwordhash = HashPassword(model.Password),
                };

                
                if (model.SelectedRole == "Admin")
                {
                    user.Role = "PendingAdmin"; 
                }
                else
                {
                    user.Role = "Customer"; 
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Chuyển hướng
                if (model.SelectedRole == "Admin")
                {
                    TempData["Message"] = "Đăng ký thành công! Vui lòng chờ Admin duyệt.";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["Message"] = "Đăng ký thành công! Mời bạn đăng nhập.";
                    return RedirectToAction("Login");
                }
            }

            return View(model);
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        // Hàm băm mật khẩu sử dụng SHA256
        private string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                
                var hash = sha256.ComputeHash(bytes);
               
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
        
    }
}