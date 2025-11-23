using System;

namespace WebDienThoai.ViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // "Admin", "Khách hàng"
        public DateTime JoinDate { get; set; }
    }
}