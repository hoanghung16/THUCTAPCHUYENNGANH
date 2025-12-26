using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebDienThoai.Models;
using WebDienThoai.ViewModels;

namespace WebDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    [Route("Admin")]
    public class AdminController(DatabaseTheKingContext context) : Controller
    {
        [Route("")]
        [Route("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var today = DateTime.Now;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            
            var monthlyRevenue = await context.Orders
                .Where(o => o.Orderdate >= startOfMonth && o.Status != "Đã hủy")
                .SumAsync(o => o.Totalprice);

            var newOrdersCount = await context.Orders
                .CountAsync(o => o.Orderdate >= startOfMonth);

            var totalProducts = await context.Products.CountAsync();
            var totalCustomers = await context.Users.CountAsync(u => u.Role == "Customer");

            
            var recentOrders = await context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.Orderdate)
                .Take(5)
                .ToListAsync();

            
            
            var topProducts = await context.OrderItems
                .GroupBy(oi => oi.Productid)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSold = g.Sum(oi => oi.Quantity)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(3)
                .Join(context.Products,
                      stat => stat.ProductId,
                      prod => prod.Id,
                      (stat, prod) => new TopProductDto
                      {
                          Name = prod.Name,
                          ImageUrl = prod.ImageUrl,
                          QuantitySold = stat.TotalSold
                      })
                .ToListAsync();

            
            var chartLabels = new string[7];
            var chartData = new decimal[7];

            for (int i = 0; i < 7; i++)
            {
                var date = today.AddDays(-6 + i).Date; 
                chartLabels[i] = date.ToString("dd/MM");

               
                chartData[i] = await context.Orders
                    .Where(o => o.Orderdate.HasValue
                                && o.Orderdate.Value.Date == date
                                && o.Status != "Đã hủy")
                    .SumAsync(o => o.Totalprice);
            }

            
            var viewModel = new DashboardViewModel
            {
                MonthlyRevenue = monthlyRevenue,
                NewOrdersCount = newOrdersCount,
                TotalProducts = totalProducts,
                TotalCustomers = totalCustomers,
                RecentOrders = recentOrders,
                TopProducts = topProducts,
                ChartLabels = chartLabels,
                ChartData = chartData
            };

            return View(viewModel);
        }

        
    }
}