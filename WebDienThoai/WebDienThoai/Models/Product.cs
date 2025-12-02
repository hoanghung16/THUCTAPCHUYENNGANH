using System.ComponentModel.DataAnnotations;

namespace WebDienThoai.Models;

public partial class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn danh mục")]
    public int Categoryid { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [StringLength(200, ErrorMessage = "Tên sản phẩm tối đa 200 ký tự")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập giá gốc")]
    [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn hoặc bằng 0")]
    public decimal Price { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Giá khuyến mãi phải lớn hơn hoặc bằng 0")]
    public decimal? Saleprice { get; set; }

    public bool IsOnSale { get; set; }
    public bool IsPublished { get; set; }

    public string? ImageUrl { get; set; }

    [StringLength(2000, ErrorMessage = "Mô tả không được quá 2000 ký tự")]
    public string? Description { get; set; }

    public virtual Category Category { get; set; } = null!;
    public virtual Inventory? Inventory { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}