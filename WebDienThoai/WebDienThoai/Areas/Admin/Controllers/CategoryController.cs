using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDienThoai.Models;

namespace WebDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(DatabaseTheKingContext context) : Controller
    {
        // 1. HIỂN THỊ DANH SÁCH
        public async Task<IActionResult> Categories()
        {
            var categories = await context.Categories.ToListAsync();
            return View(categories);
        }

        // 2. TẠO MỚI (GET - Hiển thị form)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 2. TẠO MỚI (POST - Xử lý dữ liệu)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                // Tự động tạo Slug nếu chưa có 
                if (string.IsNullOrEmpty(category.Slug))
                {
                    category.Slug = category.Name.ToLower().Replace(" ", "-");
                }

                context.Add(category);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // 3. CHỈNH SỬA (GET - Hiển thị form cũ)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // 3. CHỈNH SỬA (POST - Lưu thay đổi)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id) return NotFound();

            if (ModelState.IsValid)
            {
                context.Update(category);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Categories));
            }
            return View(category);
        }

        // 4. XÓA (GET - Xác nhận xóa)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            // KIỂM TRA QUAN TRỌNG: Nếu danh mục có sản phẩm thì chặn xóa
            var hasProduct = await context.Products.AnyAsync(p => p.Categoryid == id);
            if (hasProduct)
            {
                TempData["Error"] = "Không thể xóa danh mục này vì đang chứa sản phẩm! Hãy xóa sản phẩm trước.";
                return RedirectToAction(nameof(Categories));
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            TempData["Success"] = "Đã xóa danh mục thành công!";
            return RedirectToAction(nameof(Categories));
        }
    }
}