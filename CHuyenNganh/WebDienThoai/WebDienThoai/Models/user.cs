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
            new user { Username = "admin", Password = "123", Role = "Admin" },
        new user { Username = "khach", Password = "123", Role = "Customer" }
        };
    }
}
