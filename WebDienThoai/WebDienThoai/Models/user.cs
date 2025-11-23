namespace WebDienThoai.Models
{
    public class user
    {
        public string Username { get; set; }
        public string Password { get; set; } 
        public string FullName { get; set; }
       
        public string Role { get; set; }
    }

    public static class DemoData
    {
        public static List<user> Users = new List<user>
        {
            new user { Username = "admin", Password = "123",FullName = "Quản Trị Viên", Role = "Admin" },
        new user { Username = "khach", Password = "123",FullName = "Nguyễn Hoàng Hưng", Role = "Customer" }
        };
    }
}
