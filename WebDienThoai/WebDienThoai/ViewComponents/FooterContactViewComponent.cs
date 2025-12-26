using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text.Json;
using WebDienThoai.ViewModels; 

namespace WebDienThoai.ViewComponents
{
    public class FooterContactViewComponent : ViewComponent
    {
        private readonly IWebHostEnvironment _env;

        public FooterContactViewComponent(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IViewComponentResult Invoke()
        {
           
            string filePath = Path.Combine(_env.WebRootPath, "data", "contact.json");
            ContactInfoViewModel model;

           
            if (System.IO.File.Exists(filePath))
            {
                string jsonContent = System.IO.File.ReadAllText(filePath);
                try
                {
                    model = JsonSerializer.Deserialize<ContactInfoViewModel>(jsonContent);
                }
                catch
                {
                   
                    model = new ContactInfoViewModel();
                }
            }
            else
            {
                
                model = new ContactInfoViewModel
                {
                    Email = "contact@theking.vn",
                    Hotline = "1900 1000",
                    Address = "Đang cập nhật..."
                };
            }

           
            return View(model);
        }
    }
}