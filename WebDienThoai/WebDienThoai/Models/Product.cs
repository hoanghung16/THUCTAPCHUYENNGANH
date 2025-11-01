using WebDienThoai.Enums;

namespace WebDienThoai.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }     
        public decimal SalePrice { get; set; } 
        public bool IsOnSale { get; set; }     // Cờ để lọc sản phẩm sale

        public string Category { get; set; }
        public ProductType Type { get; set; }
    }
}