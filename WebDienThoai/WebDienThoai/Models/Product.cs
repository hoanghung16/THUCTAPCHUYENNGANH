using System;
using System.Collections.Generic;

namespace WebDienThoai.Models;

public partial class Product
{
    public int Id { get; set; }

    public int Categoryid { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal? Saleprice { get; set; }

    public bool IsOnSale { get; set; }
    public bool IsPublished { get; set; }

    public string? ImageUrl { get; set; }
    public string? Description { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Inventory? Inventory { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
