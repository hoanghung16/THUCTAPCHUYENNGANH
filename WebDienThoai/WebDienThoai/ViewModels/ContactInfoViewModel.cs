using System.ComponentModel.DataAnnotations;

namespace WebDienThoai.ViewModels
{
    public class ContactInfoViewModel
    {
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hotline không được để trống")]
        [StringLength(20, ErrorMessage = "Hotline không quá 20 ký tự")]
        public string Hotline { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string Address { get; set; }

        [Url(ErrorMessage = "Đường dẫn Fanpage không hợp lệ")]
        public string Fanpage { get; set; }
    }
}