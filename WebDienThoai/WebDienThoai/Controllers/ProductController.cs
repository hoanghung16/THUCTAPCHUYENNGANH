using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDienThoai.Models;

namespace WebDienThoai.Controllers
{
    public class ProductController(DatabaseTheKingContext context) : Controller
    {
        // 1. CHI TIẾT SẢN PHẨM
        public async Task<IActionResult> Detail(int id)
        {
            var product = await context.Products
                .Include(p => p.Category)
                
                .FirstOrDefaultAsync(m => m.Id == id && m.IsPublished == true);

            if (product == null)
            {
                return NotFound(); 
            }

            return View(product);
        }

        // 2. TRANG PHỤ KIỆN
        public async Task<IActionResult> Accessories()
        {
            var accessories = await context.Products
                .Include(p => p.Category)
                .Where(p => p.IsPublished == true)
                .Where(p => p.Category.Name == "Phụ Kiện" ||
                            p.Category.Name == "Tai Nghe" ||
                            p.Category.Name == "Ốp Lưng" ||
                            p.Category.Name == "Dây Sạc")
                .ToListAsync();

            ViewData["CategoryName"] = "Phụ Kiện Chính Hãng";
            return View("Category", accessories);
        }

        // 3. TRANG DANH MỤC (Apple, Samsung...)
        public async Task<IActionResult> Category(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName)) return NotFound();

            var products = await context.Products
                .Include(p => p.Category)
                .Where(p => p.IsPublished == true) 
                .Where(p => p.Category.Name == categoryName || p.Category.Slug == categoryName)
                .ToListAsync();

            ViewData["CategoryName"] = categoryName;
            return View(products);
        }

        // 4. TRANG SALE
        public async Task<IActionResult> Sale()
        {
            var saleProducts = await context.Products
                .Include(p => p.Category)
                .Where(p => p.IsPublished == true)
                .Where(p => p.IsOnSale == true)  
                .ToListAsync();

            ViewData["Title"] = "Sản phẩm Khuyến Mãi";
            return View("Sale", saleProducts);
        }

        public IActionResult Carts()
        {
            return View();
        }
    }
}