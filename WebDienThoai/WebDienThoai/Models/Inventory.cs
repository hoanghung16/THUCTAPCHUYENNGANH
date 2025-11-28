using System;
using System.Collections.Generic;

namespace WebDienThoai.Models;

public partial class Inventory
{
    public int ProductId { get; set; }

    public int? QuantityInStock { get; set; }

    public virtual Product Product { get; set; } = null!;
}
