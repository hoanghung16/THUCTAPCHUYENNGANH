using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDienThoai.Models;
using WebDienThoai.ViewModels;
using System.Text.Json; // [MỚI] Thư viện xử lý JSON
using System.IO;        // [MỚI] Thư viện đọc file

namespace WebDienThoai.Controllers
{
    // [CẬP NHẬT] Thêm 'IWebHostEnvironment env' vào Constructor
    public class HomeController(DatabaseTheKingContext context, IWebHostEnvironment env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var allProducts = await context.Products
                                            .Include(p => p.Category)
                                            .Where(p => p.IsPublished == true && p.Category.IsVisible == true)
                                            .AsNoTracking()
                                            .ToListAsync();

            var bestSellers = allProducts
                                  .Where(p => p.Category.Name != "Phụ Kiện" &&
                                              p.Category.Name != "Tai Nghe" &&
                                              p.Category.Name != "Ốp Lưng")
                                  .Take(4)
                                  .ToList();

            var newArrivals = allProducts
                                  .OrderByDescending(p => p.Id)
                                  .Take(4)
                                  .ToList();

            var viewModel = new HomeViewModel
            {
                BestSellers = bestSellers,
                NewArrivals = newArrivals
            };

            return View(viewModel);
        }

        
        public IActionResult Contact()
        {
            
            string filePath = Path.Combine(env.WebRootPath, "data", "contact.json");
            ContactInfoViewModel model;

            
            if (System.IO.File.Exists(filePath))
            {
                string jsonContent = System.IO.File.ReadAllText(filePath);
                try
                {
                    model = JsonSerializer.Deserialize<ContactInfoViewModel>(jsonContent);
                }
                catch
                {
                    model = new ContactInfoViewModel();
                }
            }
            else
            {
               
                model = new ContactInfoViewModel
                {
                    Address = "Đang cập nhật...",
                    Hotline = "1900 xxxx",
                    Email = "contact@theking.vn"
                };
            }

            return View(model);
        }

        // Các Action tĩnh khác
        public IActionResult GioiThieu() => View();
        public IActionResult HeThongCuaHang() => View();
        public IActionResult TuyenDung() => View();
        public IActionResult FAQ() => View();
        public IActionResult ChinhSachVanChuyen() => View();
        public IActionResult ChinhSachDoiTra() => View();
        public IActionResult DieuKhoanDichVu() => View();
        public IActionResult ChinhSachBaoMat() => View();
    }
}