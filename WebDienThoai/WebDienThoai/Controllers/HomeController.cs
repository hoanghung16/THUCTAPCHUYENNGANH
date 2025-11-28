using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDienThoai.Models;
using WebDienThoai.ViewModels;

namespace WebDienThoai.Controllers
{
    public class HomeController(DatabaseTheKingContext context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            
            var allProducts = await context.Products
                                            .Include(p => p.Category)
                                            .Where(p => p.IsPublished == true) 
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