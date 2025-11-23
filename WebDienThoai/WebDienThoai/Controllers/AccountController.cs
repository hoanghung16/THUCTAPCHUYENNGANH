using Microsoft.AspNetCore.Mvc;
using WebDienThoai.Models;
using WebDienThoai.ViewModels;

namespace WebDienThoai.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChuyenTrang(LoginViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View("Login",model);
            }
            user user = DemoData.Users
                .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user != null)
            {
                if (user.Role == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin", new {area = "Admin"});
                }
                else if (user.Role == "Customer")
                {
                    return RedirectToAction("Index", "Home", new {area = ""});
                }
            }
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
            return View("Login",model);
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}
