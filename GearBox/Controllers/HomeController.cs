using BLL.InterFaces;
using GearBox.Models; // أو DAL.Models (تأكد من الـ Namespace عندك)
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GearBox.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            // 1. لو المستخدم "مزود خدمة"، يروح تلقائي للوحة التحكم الخاصة به
            if (User.IsInRole("Provider"))
            {
                
                return RedirectToAction("ManageBookings", "Provider");
            }

            
            if (User.IsInRole("Client"))
            {
                
            }

            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}