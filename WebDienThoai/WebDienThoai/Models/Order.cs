using System;
using System.Collections.Generic;

namespace WebDienThoai.Models;

public partial class Order
{
    public int Id { get; set; }

    public int Userid { get; set; }

    public DateTime? Orderdate { get; set; }

    public string? Status { get; set; }

    public string? Paymentstatus { get; set; }

    public decimal Totalprice { get; set; }

    public string? Shipname { get; set; }

    public string? Shipaddress { get; set; }

    public string? Shipphone { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
