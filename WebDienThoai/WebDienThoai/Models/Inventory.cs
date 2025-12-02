using System.ComponentModel.DataAnnotations;

namespace WebDienThoai.Models;

public partial class Inventory
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số lượng tồn")]
    [Range(0, 10000, ErrorMessage = "Số lượng tồn phải từ 0 đến 10.000")]
    public int? QuantityInStock { get; set; }

    public virtual Product Product { get; set; } = null!;
}