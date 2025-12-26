using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDienThoai.Models;
using WebDienThoai.ViewModels;

namespace WebDienThoai.Controllers
{
    public class ProductController(DatabaseTheKingContext context) : Controller
    {
        // 1. TRANG CỬA HÀNG (SHOP)
        public async Task<IActionResult> Shop(int page = 1, string categorySlug = "all", string priceRange = "all", string sort = "default")
        {
            int pageSize = 9;

            
            var query = context.Products
                .Include(p => p.Category)
                .Where(p => p.IsPublished == true && p.Category.IsVisible == true)
                .AsQueryable();

            
            if (!string.IsNullOrEmpty(categorySlug) && categorySlug != "all")
            {
                if (categorySlug == "phu-kien" || categorySlug == "Phụ Kiện")
                {
                    string[] phoneBrands = { "Apple", "Samsung", "Xiaomi", "Oppo", "Realme", "Vivo", "Iphone", "Sony", "Nokia", "Asus" };
                    query = query.Where(p => !phoneBrands.Contains(p.Category.Name));
                }
                else
                {
                    query = query.Where(p => p.Category.Slug == categorySlug || p.Category.Name == categorySlug);
                }
            }

            
            switch (priceRange)
            {
                case "under-5":
                    query = query.Where(p => (p.IsOnSale && p.Saleprice.HasValue ? p.Saleprice.Value : p.Price) < 5000000);
                    break;
                case "5-15":
                    query = query.Where(p => (p.IsOnSale && p.Saleprice.HasValue ? p.Saleprice.Value : p.Price) >= 5000000
                                          && (p.IsOnSale && p.Saleprice.HasValue ? p.Saleprice.Value : p.Price) <= 15000000);
                    break;
                case "15-25":
                    query = query.Where(p => (p.IsOnSale && p.Saleprice.HasValue ? p.Saleprice.Value : p.Price) > 15000000
                                          && (p.IsOnSale && p.Saleprice.HasValue ? p.Saleprice.Value : p.Price) <= 25000000);
                    break;
                case "over-25":
                    query = query.Where(p => (p.IsOnSale && p.Saleprice.HasValue ? p.Saleprice.Value : p.Price) > 25000000);
                    break;
            }

           
            switch (sort)
            {
                case "price_asc":
                    query = query.OrderBy(p => (p.IsOnSale && p.Saleprice.HasValue ? p.Saleprice.Value : p.Price));
                    break;
                case "price_desc":
                    query = query.OrderByDescending(p => (p.IsOnSale && p.Saleprice.HasValue ? p.Saleprice.Value : p.Price));
                    break;
                case "name_asc":
                    query = query.OrderBy(p => p.Name);
                    break;
                default:
                    query = query.OrderByDescending(p => p.Id);
                    break;
            }

            // --- Phân trang ---
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            if (page < 1) page = 1;
            if (page > totalPages && totalPages > 0) page = totalPages;

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

           
            var categories = await context.Categories
                .Where(c => c.IsVisible == true)
                .Include(c => c.Products).ToListAsync();

            var viewModel = new ShopViewModel
            {
                Products = products,
                Categories = categories,
                CurrentPage = page,
                TotalPages = totalPages,
                CurrentCategory = categorySlug,
                CurrentPriceRange = priceRange,
                CurrentSort = sort
            };

            return View(viewModel);
        }

        // 2. CHI TIẾT SẢN PHẨM
        public async Task<IActionResult> Detail(int id)
        {
            
            var product = await context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id && m.IsPublished == true && m.Category.IsVisible == true);

            if (product == null) return NotFound();

            
            var relatedProducts = await context.Products
                .Where(p => p.Category.Id == product.Category.Id && p.Id != id && p.IsPublished == true)
                .OrderBy(r => Guid.NewGuid())
                .Take(4)
                .ToListAsync();

            ViewBag.RelatedProducts = relatedProducts;

            return View(product);
        }

        // 3. CÁC ACTION REDIRECT 
        public IActionResult Category(string categoryName)
        {
            return RedirectToAction("Shop", new { categorySlug = categoryName });
        }

        public IActionResult Accessories()
        {
            return RedirectToAction("Shop", new { categorySlug = "phu-kien" });
        }

        public async Task<IActionResult> Sale()
        {
            
            var saleProducts = await context.Products
                .Include(p => p.Category)
                .Where(p => p.IsPublished == true && p.IsOnSale == true && p.Category.IsVisible == true)
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