using System.ComponentModel.DataAnnotations;

namespace WebDienThoai.Models;

public partial class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập từ 3 đến 50 ký tự")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Họ tên không được để trống")]
    public string? Fullname { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Email")]
    [EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
    [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
    public string Passwordhash { get; set; } = null!;

    public string? Role { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}