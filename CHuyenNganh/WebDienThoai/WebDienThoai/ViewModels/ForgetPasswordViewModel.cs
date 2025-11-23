using System.ComponentModel.DataAnnotations;

namespace WebDienThoai.ViewModels 
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
    }
}