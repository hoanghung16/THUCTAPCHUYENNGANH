using WebDienThoai.Models;

namespace WebDienThoai.ViewModels
{
    public class ShopViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        
        public string CurrentCategory { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentPriceRange { get; set; }
    }
}