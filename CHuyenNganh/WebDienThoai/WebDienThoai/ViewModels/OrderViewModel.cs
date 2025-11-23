using System;

namespace WebDienThoai.ViewModels
{
    public class OrderViewModel
    {
        public string OrderId { get; set; } 
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } // "Đang xử lý", "Đã giao", "Đã hủy"
    }
}