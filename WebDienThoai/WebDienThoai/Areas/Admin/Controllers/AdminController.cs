using Microsoft.AspNetCore.Mvc;
using WebDienThoai.ViewModels;

namespace WebDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult Categories()
        {
            var categoryList = new List<CategoryViewModel> 
        {
            new CategoryViewModel { Id = 1, Name = "Apple", Slug = "apple", ProductCount = 3 },
            new CategoryViewModel { Id = 2, Name = "Samsung", Slug = "samsung", ProductCount = 1 },
            new CategoryViewModel { Id = 3, Name = "Xiaomi", Slug = "xiaomi", ProductCount = 1 },
            new CategoryViewModel { Id = 4, Name = "Tai Nghe", Slug = "tainghe", ProductCount = 1 },
            new CategoryViewModel { Id = 5, Name = "Ốp Lưng", Slug = "oplung", ProductCount = 2 },
            new CategoryViewModel { Id = 6, Name = "Dây Sạc", Slug = "daysac", ProductCount = 1 }
        };

            ViewData["Title"] = "Quản lý Danh mục";
            return View(categoryList); 
        }
        public IActionResult Orders()
        {
            ViewData["Title"] = "Quản lý Đơn hàng";
            var orderList = new List<OrderViewModel>
    {
        new OrderViewModel { OrderId = "#12345", CustomerName = "Nguyễn Văn A", OrderDate = DateTime.Now.AddDays(-1), TotalPrice = 27590000m, Status = "Đang xử lý" },
        new OrderViewModel { OrderId = "#12344", CustomerName = "Trần Thị B", OrderDate = DateTime.Now.AddDays(-2), TotalPrice = 5490000m, Status = "Đã giao" },
        new OrderViewModel { OrderId = "#12343", CustomerName = "Lê Văn C", OrderDate = DateTime.Now.AddDays(-3), TotalPrice = 750000m, Status = "Đã hủy" },
    };
            return View(orderList); 
        }
        public IActionResult Users()
        {
            ViewData["Title"] = "Quản lý Người dùng";
            var userList = new List<UserViewModel>
    {
        new UserViewModel { UserId = "u-001", FullName = "Admin", Email = "admin@webdienthoai.vn", Role = "Admin", JoinDate = DateTime.Now.AddMonths(-6) },
        new UserViewModel { UserId = "u-002", FullName = "Nguyễn Văn A", Email = "nguyenvana@gmail.com", Role = "Khách hàng", JoinDate = DateTime.Now.AddDays(-2) },
        new UserViewModel { UserId = "u-003", FullName = "Trần Thị B", Email = "tranthib@gmail.com", Role = "Khách hàng", JoinDate = DateTime.Now.AddDays(-5) },
    };
            return View(userList); 
        }
    }
}
