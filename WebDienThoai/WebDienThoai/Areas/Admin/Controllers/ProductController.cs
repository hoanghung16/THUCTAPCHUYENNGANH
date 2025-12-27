using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDienThoai.Models;

namespace WebDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class ProductController(DatabaseTheKingContext context, IWebHostEnvironment webHostEnvironment) : Controller
    {
        // 1. HIỂN THỊ DANH SÁCH
        public async Task<IActionResult> Products()
        {
            var products = await context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
            return View(products);
        }

        // 2. TẠO MỚI (GET)
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name");
            return View();
        }

        // 3. TẠO MỚI (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
        {
            
            ModelState.Remove("Category");
            ModelState.Remove("Inventory");
            ModelState.Remove("OrderItems");

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    
                    string extension = Path.GetExtension(imageFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + extension;

                  
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = "/img/" + uniqueFileName;
                }

                context.Add(product);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Products));
            }

            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name", product.Categoryid);
            return View(product);
        }

        // 4. CHỈNH SỬA (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name", product.Categoryid);
            return View(product);
        }

        // 5. CHỈNH SỬA (POST) - CÓ XÓA ẢNH CŨ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile? imageFile)
        {
            if (id != product.Id) return NotFound();

            ModelState.Remove("Category");
            ModelState.Remove("Inventory");
            ModelState.Remove("OrderItems");

            if (ModelState.IsValid)
            {
                try
                {
                    
                    var existingProduct = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                    if (imageFile != null)
                    {
                        
                        if (existingProduct != null && !string.IsNullOrEmpty(existingProduct.ImageUrl))
                        {
                            string oldFilePath = Path.Combine(webHostEnvironment.WebRootPath, existingProduct.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldFilePath)) System.IO.File.Delete(oldFilePath);
                        }

                       
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "img", uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }
                        product.ImageUrl = "/img/" + uniqueFileName;
                    }
                    else
                    {
                        
                        if (existingProduct != null) product.ImageUrl = existingProduct.ImageUrl;
                    }

                    context.Update(product);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.Products.Any(e => e.Id == product.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Products));
            }

            ViewBag.Categories = new SelectList(context.Categories, "Id", "Name", product.Categoryid);
            return View(product);
        }

        // 6. XÓA (DELETE) - CÓ XÓA ẢNH TRÊN SERVER
        public async Task<IActionResult> Delete(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product != null)
            {
                // Xóa file ảnh vật lý
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
                }

                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Products));
        }
    }
}