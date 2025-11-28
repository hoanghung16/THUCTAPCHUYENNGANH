using WebDienThoai.Models;

namespace WebDienThoai.ViewModels
{
    public class DashboardViewModel
    {
        
        public decimal MonthlyRevenue { get; set; } // Doanh thu tháng này
        public int NewOrdersCount { get; set; }     // Số đơn tháng này
        public int TotalProducts { get; set; }      // Tổng sản phẩm
        public int TotalCustomers { get; set; }     // Tổng khách hàng

        // Danh sách
        public List<Order> RecentOrders { get; set; } // Đơn hàng gần đây
        public List<TopProductDto> TopProducts { get; set; } // Top sản phẩm bán chạy

        // Dữ liệu biểu đồ (Chart)
        public string[] ChartLabels { get; set; } // Nhãn ngày (Thứ 2, Thứ 3...)
        public decimal[] ChartData { get; set; }  // Dữ liệu doanh thu tương ứng
    }

    public class TopProductDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int QuantitySold { get; set; }
    }
}