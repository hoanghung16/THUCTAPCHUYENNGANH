using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDienThoai.Models;

namespace WebDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class OrderController(DatabaseTheKingContext context) : Controller
    {
        // 1. DANH SÁCH ĐƠN HÀNG
        public async Task<IActionResult> Orders()
        {
            var orders = await context.Orders
                .Include(o => o.User) // Lấy thông tin người đặt
                .OrderByDescending(o => o.Orderdate) // Mới nhất lên đầu
                .ToListAsync();
            return View(orders);
        }

        // 2. XEM CHI TIẾT ĐƠN HÀNG
        public async Task<IActionResult> Details(int id)
        {
            var order = await context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product) // Lấy thông tin sản phẩm trong đơn
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        // 3. CẬP NHẬT TRẠNG THÁI (Duyệt đơn / Giao hàng / Hủy)
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var order = await context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = status;

            
                if (status == "Đã giao")
                {
                    order.Paymentstatus = "Đã thanh toán";
                    order.Orderdate = DateTime.Now; // Cập nhật ngày thực tế
                }

                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}