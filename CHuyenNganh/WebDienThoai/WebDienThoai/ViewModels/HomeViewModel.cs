using WebDienThoai.Models; 
using System.Collections.Generic;

namespace WebDienThoai.ViewModels 
{
    public class HomeViewModel
    {
        public List<Product> BestSellers { get; set; }
        public List<Product> NewArrivals { get; set; }
    }
}