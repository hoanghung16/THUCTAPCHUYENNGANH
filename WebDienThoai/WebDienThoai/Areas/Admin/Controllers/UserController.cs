using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDienThoai.Models;

namespace WebDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController(DatabaseTheKingContext context) : Controller
    {
        public async Task<IActionResult> Users()
        {
            var users = await context.Users.ToListAsync();
            return View(users);
        }

      
        public async Task<IActionResult> Delete(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                
                if (user.Role == "Admin")
                {
                    TempData["Error"] = "Không thể xóa tài khoản Admin!";
                }
                else
                {
                    context.Users.Remove(user);
                    await context.SaveChangesAsync();
                }
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
            }
            return RedirectToAction("Users"); 
        }
    }
}