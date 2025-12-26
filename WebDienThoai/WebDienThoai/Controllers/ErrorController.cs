using Microsoft.AspNetCore.Mvc;

namespace WebDienThoai.Controllers
{
    public class ErrorController : Controller
    {
        
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewData["ErrorMessage"] = "Xin lỗi, trang bạn tìm kiếm không tồn tại hoặc đã bị xóa.";
                    return View("NotFound"); 

                case 500:
                    ViewData["ErrorMessage"] = "Hệ thống đang gặp sự cố. Vui lòng thử lại sau.";
                    break;
            }

            return View("NotFound"); 
        }
    }
}