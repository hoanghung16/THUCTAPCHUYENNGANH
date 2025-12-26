using Microsoft.AspNetCore.Mvc;
using WebDienThoai.ViewModels;
using System.Text.Json; 
using Microsoft.AspNetCore.Hosting; 

namespace WebDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactInfoController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public ContactInfoController(IWebHostEnvironment env)
        {
            _env = env;
        }

      
        private string GetDataFilePath()
        {
            string folderPath = Path.Combine(_env.WebRootPath, "data");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath); 
            return Path.Combine(folderPath, "contact.json");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var filePath = GetDataFilePath();
            ContactInfoViewModel model;

            if (System.IO.File.Exists(filePath))
            {
                
                string jsonContent = System.IO.File.ReadAllText(filePath);
                model = JsonSerializer.Deserialize<ContactInfoViewModel>(jsonContent);
            }
            else
            {
                
                model = new ContactInfoViewModel
                {
                    Email = "contact@theking.vn",
                    Hotline = "1900 1000",
                    Address = "Đang cập nhật...",
                    Fanpage = "#"
                };
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ContactInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                string jsonContent = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true });

                
                var filePath = GetDataFilePath();
                System.IO.File.WriteAllText(filePath, jsonContent);

                TempData["Message"] = "Lưu cấu hình thành công!";
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }
    }
}