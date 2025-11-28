using Microsoft.AspNetCore.Mvc;
using WebDienThoai.Helpers;
using WebDienThoai.Models;
using WebDienThoai.ViewModels;

namespace WebDienThoai.Controllers
{
    public class CartController(DatabaseTheKingContext context) : Controller
    {
        // 1. HIỂN THỊ GIỎ HÀNG
        public IActionResult Index()
        {
            // --- KIỂM TRA ĐĂNG NHẬP ---
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // ---------------------------

            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }

        // 2. THÊM VÀO GIỎ
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            // --- KIỂM TRA ĐĂNG NHẬP ---
            if (HttpContext.Session.GetString("Username") == null)
            {
                // Mẹo: Lưu thông báo để hiển thị bên trang Login
                TempData["Message"] = "Vui lòng đăng nhập để mua hàng!";
                return RedirectToAction("Login", "Account");
            }
            // ---------------------------

            var product = context.Products.Find(id);
            if (product == null) return NotFound();

            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var existingItem = cart.FirstOrDefault(x => x.ProductId == id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                decimal finalPrice = (product.IsOnSale && product.Saleprice.HasValue)
                                     ? product.Saleprice.Value
                                     : product.Price;

                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ImageUrl = product.ImageUrl ?? "/img/no-image.png",
                    Price = finalPrice,
                    Quantity = quantity
                });
            }

            HttpContext.Session.Set("Cart", cart);
            return RedirectToAction("Index");
        }

        // 3. XÓA KHỎI GIỎ
        public IActionResult Remove(int id)
        {
            // --- KIỂM TRA ĐĂNG NHẬP ---
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // ---------------------------

            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.Set("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        // 4. CẬP NHẬT SỐ LƯỢNG
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            // --- KIỂM TRA ĐĂNG NHẬP ---
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // ---------------------------

            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                item.Quantity = quantity;
                if (item.Quantity <= 0) cart.Remove(item);
            }

            HttpContext.Session.Set("Cart", cart);
            return RedirectToAction("Index");
        }
    }
}