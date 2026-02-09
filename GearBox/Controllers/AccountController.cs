using BLL.InterFaces;
using BLL.View_models;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GearBox.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager; // تعريف المتغير

        // 3. حقن UserManager في الكونستركتور
        public AccountController(IAccountService accountService, UserManager<ApplicationUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ClientRegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.RegisterClientAsync(model);

                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Client");
                }


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(ClientLoginVM model)
        {
            if (ModelState.IsValid)
            {
                // 1. محاولة تسجيل الدخول
                var result = await _accountService.LoginAsync(model);

                if (result.Succeeded)
                {
                    // 2. نجيب بيانات اليوزر عشان نعرف الرول بتاعته
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    // 3. فحص الرول والتوجيه
                    if (await _userManager.IsInRoleAsync(user, "Provider"))
                    {
                        // لو بروفيدر -> وديه Dashboard
                        return RedirectToAction("Index", "Provider");
                    }
                    else
                    {
                        // لو عميل -> وديه الصفحة الرئيسية
                        return RedirectToAction("Index", "Client");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();
            return RedirectToAction("Login");
        }

        //Provider
        [HttpGet]
        public IActionResult RegisterProvider()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterProvider(ProviderRegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.RegisterProviderAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Provider");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

    }
}
