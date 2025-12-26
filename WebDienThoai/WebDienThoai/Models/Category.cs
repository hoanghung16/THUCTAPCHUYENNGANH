using System.ComponentModel.DataAnnotations;

namespace WebDienThoai.Models;

public partial class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
    [StringLength(100, ErrorMessage = "Tên danh mục không được quá 100 ký tự")]
    public string Name { get; set; } = null!;

    public string? Slug { get; set; }
    public bool IsVisible { get; set; } = true;
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}