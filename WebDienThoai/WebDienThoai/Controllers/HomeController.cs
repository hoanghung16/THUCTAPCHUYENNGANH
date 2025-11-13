using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 
using System.Diagnostics;
using System.Linq;
using WebDienThoai.Enums;
using WebDienThoai.Models;
using WebDienThoai.ViewModels;

namespace WebTHEKING.Controllers
{
    public class HomeController : Controller
    {
            // Điện thoại
           private static List<Product> _allProducts = new List<Product>
        {
            new Product
        {
            Id = "1",
            Name = "iPhone 15 Pro",
            Description = "Titanium",
            ImageUrl = "/img/10.jpg",
            Price = 28990000m,       // Giá cũ
            SalePrice = 27590000m,   // Giá mới (giảm giá)
            IsOnSale = true,         // Đang sale
            Category = "Apple",
            Type = ProductType.Phone
        },
        
        // SẢN PHẨM KHÔNG SALE
        new Product
        {
            Id = "2",
            Name = "Samsung S25",
            Description = "Advanced AI",
            ImageUrl = "/img/20.jpg",
            Price = 27500000m,       // Giá cũ
            SalePrice = 27500000m,   // Giá mới (bằng giá cũ)
            IsOnSale = false,        // Không sale
            Category = "Samsung",
            Type = ProductType.Phone
        },

        new Product
        {
            Id = "3",
            Name = "Iphone 13",
            Description = "Leica camera",
            ImageUrl = "/img/9.jpg",
            Price = 22990000m,
            SalePrice = 22990000m,
            IsOnSale = false,
            Category = "Apple",
            Type = ProductType.Phone
        },

        // SẢN PHẨM SALE
        new Product
        {
            Id = "4",
            Name = "iPhone 14",
            Description = "Camera cải tiến",
            ImageUrl = "/img/19.jpg",
            Price = 13790000m,       
            SalePrice = 12990000m,   
            IsOnSale = true,         
            Category = "Apple",
            Type = ProductType.Phone
        },
        new Product
        {
            Id = "9", 
            Name = "iPhone 17",
            Description = "Tương lai là đây",
            ImageUrl = "/img/iphone-17.jpg", 
            Price = 34990000m,      
            SalePrice = 34990000m,   
            IsOnSale = false,        
            Category = "Apple",
            Type = ProductType.Phone
        },

        // --- PHỤ KIỆN ---

        // SẢN PHẨM SALE
        new Product
        {
            Id = "5",
            Name = "AirPods Pro 2",
            Description = "Noise Cancellation",
            ImageUrl = "/img/14.jpg",
            Price = 6190000m,        
            SalePrice = 5490000m,   
            IsOnSale = true,        
            Category = "tainghe",
            Type = ProductType.Accessory
        },

        new Product
        {
            Id = "6",
            Name = "Ốp lưng Spigen",
            Description = "Bảo vệ toàn diện",
            ImageUrl = "/img/22.jpg",
            Price = 750000m,
            SalePrice = 750000m,
            IsOnSale = false,
            Category = "oplung",
            Type = ProductType.Accessory
        },

        new Product
        {
            Id = "7",
            Name = "Cáp Type C Anker",
            Description = "Bền bỉ, sạc nhanh",
            ImageUrl = "/img/26.jpg",
            Price = 350000m,
            SalePrice = 350000m,
            IsOnSale = false,
            Category = "daysac",
            Type = ProductType.Accessory
        },

        new Product
        {
            Id = "8",
            Name = "Ốp lưng iPhone 15",
            Description = "Trong suốt",
            ImageUrl = "/img/21.jpg",
            Price = 450000m,
            SalePrice = 450000m,
            IsOnSale = false,
            Category = "oplung",
            Type = ProductType.Accessory
        }
    };
        // ===================================

        public IActionResult Index()
        {
           
            var bestSellers = _allProducts
                                  .Where(p => p.Type == ProductType.Phone)
                                  .Take(4)
                                  .ToList();

           
            var newArrivals = _allProducts
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
        public IActionResult GioiThieu()
        {
            ViewData["Title"] = "Giới thiệu";
            return View();
        }

        public IActionResult HeThongCuaHang()
        {
            ViewData["Title"] = "Hệ thống cửa hàng";
            return View();
        }

        public IActionResult TuyenDung()
        {
            ViewData["Title"] = "Tuyển dụng";
            return View();
        }

        // --- HỖ TRỢ KHÁCH HÀNG ---
        public IActionResult FAQ()
        {
            ViewData["Title"] = "Câu hỏi thường gặp";
            return View();
        }

        public IActionResult ChinhSachVanChuyen()
        {
            ViewData["Title"] = "Chính sách vận chuyển";
            return View();
        }

        public IActionResult ChinhSachDoiTra()
        {
            ViewData["Title"] = "Chính sách đổi trả";
            return View();
        }

        // --- THÔNG TIN PHÁP LÝ ---
        public IActionResult DieuKhoanDichVu()
        {
            ViewData["Title"] = "Điều khoản dịch vụ";
            return View();
        }

        public IActionResult ChinhSachBaoMat()
        {
            ViewData["Title"] = "Chính sách bảo mật";
            return View();
        }
    }
}