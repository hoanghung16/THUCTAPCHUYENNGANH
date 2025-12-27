using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDienThoai.Models;

namespace WebDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class UserController(DatabaseTheKingContext context) : Controller
    {
        public async Task<IActionResult> Users()
        {
            var users = await context.Users.ToListAsync();
            return View(users);
        }


        public async Task<IActionResult> Delete(int id)
        {
           
            var user = await context.Users
                                    .Include(u => u.Orders)
                                    .FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
               
                if (user.Role == "Admin")
                {
                    TempData["Error"] = "Không thể xóa tài khoản Admin!";
                }
                
                else if (user.Orders.Any())
                {
                    TempData["Error"] = $"Không thể xoá thành viên {user.Username} vì họ đã có lịch sử mua hàng.";
                }
             
                else
                {
                    try
                    {
                        context.Users.Remove(user);
                        await context.SaveChangesAsync();
                        TempData["Success"] = "Đã xóa người dùng thành công!";
                    }
                    catch (Exception ex)
                    {
                        
                        TempData["Error"] = "Lỗi hệ thống: Không thể xoá người dùng này.";
                    }
                }
            }
            else
            {
                TempData["Error"] = "Người dùng không tồn tại!";
            }

            return RedirectToAction(nameof(Users));
        }
        public async Task<IActionResult> ApproveUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null && user.Role == "PendingAdmin")
            {
                
                user.Role = "Admin";
                await context.SaveChangesAsync();
                TempData["Success"] = $"Đã duyệt thành viên {user.Username} lên làm Admin!";
            }
            return RedirectToAction("Users"); 
        }
       
    }
}