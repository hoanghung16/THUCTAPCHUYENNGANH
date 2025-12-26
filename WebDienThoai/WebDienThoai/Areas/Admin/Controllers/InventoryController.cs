using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDienThoai.Models;

namespace WebDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class InventoryController(DatabaseTheKingContext context) : Controller
    {
        // 1. DANH SÁCH TỒN KHO
        public async Task<IActionResult> Inventory()
        {
            // Lấy tất cả sản phẩm, kèm thông tin kho
            var products = await context.Products
                .Include(p => p.Inventory)
                .OrderBy(p => p.Name)
                .ToListAsync();
            return View(products);
        }

        // 2. CẬP NHẬT KHO (GET)
        public async Task<IActionResult> Edit(int id) // id ở đây là ProductId
        {
            var product = await context.Products
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            // Nếu chưa có dòng inventory nào, tạo object ảo để hiển thị
            var inventory = product.Inventory ?? new Inventory { ProductId = id, QuantityInStock = 0 };

           
            ViewBag.ProductName = product.Name;
            ViewBag.ProductImage = product.ImageUrl;

            return View(inventory);
        }

        // 3. CẬP NHẬT KHO (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ProductId, int QuantityInStock)
        {
            var inventory = await context.Inventories.FindAsync(ProductId);

            if (inventory != null)
            {
                // Nếu đã có -> Cập nhật
                inventory.QuantityInStock = QuantityInStock;
                context.Update(inventory);
            }
            else
            {
                // Nếu chưa có -> Tạo mới
                var newInventory = new Inventory
                {
                    ProductId = ProductId,
                    QuantityInStock = QuantityInStock
                };
                context.Add(newInventory);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Inventory));
        }
    }
}