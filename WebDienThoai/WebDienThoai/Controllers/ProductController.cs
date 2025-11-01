using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebDienThoai.Models;     
using WebDienThoai.Enums;     
using System.Collections.Generic;

namespace WebDienThoai.Controllers 
{
    public class ProductController : Controller
    {
        // === DỮ LIỆU GIẢ LẬP DATABASE ===
        private static List<Product> _allProducts = new List<Product>
        {
            new Product
        {
            Id = 1,
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
            Id = 2,
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
            Id = 10,
            Name = "iPhone 14",
            Description = "Leica camera",
            ImageUrl = "/img/19.jpg",
            Price = 22990000m,
            SalePrice = 22990000m,
            IsOnSale = false,
            Category = "Apple",
            Type = ProductType.Phone
        },

        new Product
        {
            Id = 3,
            Name = "Xiaomi 15",
            Description = "Leica camera",
            ImageUrl = "/img/17.jpg",
            Price = 22990000m,
            SalePrice = 22990000m,
            IsOnSale = false,
            Category = "Xiaomi",
            Type = ProductType.Phone
        },

        // SẢN PHẨM SALE
        new Product
        {
            Id = 4,
            Name = "iPhone 13",
            Description = "Camera cải tiến",
            ImageUrl = "/img/9.jpg",
            Price = 13790000m,       // Giá cũ
            SalePrice = 12990000m,   // Giá mới
            IsOnSale = true,         // Đang sale
            Category = "Apple",
            Type = ProductType.Phone
        },
        new Product
        {
            Id = 9,
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
            Id = 5,
            Name = "AirPods Pro 2",
            Description = "Noise Cancellation",
            ImageUrl = "/img/14.jpg",
            Price = 6190000m,        // Giá cũ
            SalePrice = 5490000m,    // Giá mới
            IsOnSale = true,         // Đang sale
            Category = "tainghe",
            Type = ProductType.Accessory
        },

        new Product
        {
            Id = 6,
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
            Id = 7,
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
            Id = 8,
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


        // Hiển thị trang chi tiết cho một sản phẩm
        public IActionResult Detail(int id)
        {
            var product = _allProducts.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product); // Bạn sẽ cần tạo View "Detail.cshtml"
        }

        // GET: /Product/Accessories
        // Hiển thị trang chứa tất cả phụ kiện
        public IActionResult Accessories()
        {
            var accessories = _allProducts
                                  .Where(p => p.Type == ProductType.Accessory)
                                  .ToList();

            ViewData["CategoryName"] = "Phụ Kiện";
            return View("Category", accessories); 
        }

        // GET: /Product/Category/Apple
        // Hiển thị trang chứa sản phẩm theo danh mục
        public IActionResult Category(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return NotFound();
            }

            var products = _allProducts
                               .Where(p => p.Category == categoryName)
                               .ToList();

            ViewData["CategoryName"] = categoryName;
            return View(products); // Bạn sẽ cần tạo View "Category.cshtml"
        }

        // GET: /Product/Sale
        // (Tạm thời) Chuyển hướng đến trang phụ kiện khi nhấn "Sale"
        public IActionResult Sale()
        {
            var allProducts = _allProducts;

            // 2. Lọc ra chỉ những sản phẩm có IsOnSale == true
            var saleProducts = allProducts
                                .Where(p => p.IsOnSale == true)
                                .ToList();

            // 3. Đặt tiêu đề (giống như bạn đã làm)
            ViewData["Title"] = "Sản phẩm Khuyến Mãi";

            // 4. Trả về View tên "Sale" và gửi danh sách đã lọc cho nó
            // File Sale.cshtml của bạn sẽ nhận danh sách này
            return View("Sale", saleProducts);
        }
    }
}