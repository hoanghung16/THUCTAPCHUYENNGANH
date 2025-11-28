using System;
using System.Collections.Generic;

namespace WebDienThoai.Models;

public partial class OrderItem
{
    public int Orderid { get; set; }

    public int Productid { get; set; }

    public int Quantity { get; set; }

    public decimal Unitprice { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
