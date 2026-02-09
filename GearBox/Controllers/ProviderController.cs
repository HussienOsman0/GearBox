using BLL.InterFaces;
using BLL.View_models;
using DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GearBox.Controllers
{
    [Authorize(Roles = "Provider")] 
    public class ProviderController : Controller
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            
            var bookings = await _providerService.GetIncomingBookingsAsync(userId);

            
            ViewBag.PendingCount = bookings.Count(b => b.Status == DAL.Enums.BookingStatus.Pending);
            ViewBag.TotalBookings = bookings.Count();
            ViewBag.TodayBookings = bookings.Count(b => b.BookingDate.Date == DateTime.Today);

            
            return View(bookings.OrderByDescending(b => b.BookingDate).Take(5));
        }


        [HttpGet]
        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService(ServiceVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _providerService.AddServiceAsync(model, userId);
                return RedirectToAction("MyServices");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MyServices()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var services = await _providerService.GetMyServicesAsync(userId);
            return View(services);
        }

        
        [HttpGet]
        public async Task<IActionResult> ManageBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookings = await _providerService.GetIncomingBookingsAsync(userId);
            return View(bookings);
        }

        
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int bookingId, BookingStatus status)
        {
            await _providerService.ChangeBookingStatusAsync(bookingId, status);
            return RedirectToAction("ManageBookings");
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await _providerService.GetProfileAsync(userId);
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(ProviderProfileVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _providerService.UpdateProfileAsync(model, userId);

            if (result)
            {
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
