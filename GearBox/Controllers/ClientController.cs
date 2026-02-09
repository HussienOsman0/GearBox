using BLL.InterFaces;
using BLL.View_models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace GearBox.Controllers
{
    [Authorize(Roles = "Client")] 
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IUnitOfWork _unitOfWork;

        public ClientController(IClientService clientService, IUnitOfWork unitOfWork)
        {
            _clientService = clientService;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            
            var bookings = await _clientService.GetMyBookingsAsync(userId);
            var vehicles = await _clientService.GetMyVehiclesAsync(userId);

           
            var providers = await _unitOfWork.Providers.FindAsync(p => true, new[] { "Services" });

            
            var model = new ClientHomeVM
            {
                ClientName = User.Identity.Name,
                Bookings = bookings.OrderByDescending(b => b.BookingDate),
                Vehicles = vehicles,
                Providers = providers 
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult AddVehicle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicle(VehicleVM model)
        {
            if (ModelState.IsValid)
            {
                
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _clientService.AddVehicleAsync(model, userId);
                return RedirectToAction("Index", "Client"); 
            }
            return View(model);
        }

        
        [HttpGet]
        public async Task<IActionResult> BookService(int providerId)
        {
            var provider = await _unitOfWork.Providers.GetByIdAsync(providerId);
            if (provider == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myVehicles = await _clientService.GetMyVehiclesAsync(userId);

            
            var model = new BookingVM
            {
                ProviderId = providerId,
                ProviderName = provider.FullName
            };

            
            ViewBag.Vehicles = new SelectList(myVehicles, "Id", "make");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookService(BookingVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = await _clientService.CreateBookingAsync(model, userId);

                if (result)
                {
                    return RedirectToAction("Index", "Client"); 
                }
            }

            
            var userIdForReload = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myVehicles = await _clientService.GetMyVehiclesAsync(userIdForReload);
            ViewBag.Vehicles = new SelectList(myVehicles, "Id", "make");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await _clientService.GetProfileAsync(userId);
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(ClientProfileVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _clientService.UpdateProfileAsync(model, userId);

            if (result)
            {
                TempData["SuccessMessage"] = "Profile updated successfully!"; 
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error updating profile.");
            return View(model);
        }
    }
}
