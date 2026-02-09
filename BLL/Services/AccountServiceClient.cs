using BLL.InterFaces; 
using BLL.InterFaces;
using BLL.View_models;
using BLL.View_models; 
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountServiceClient : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountServiceClient(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        // Register Client
        public async Task<IdentityResult> RegisterClientAsync(ClientRegisterVM model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Client"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Client"));
                }


                await _userManager.AddToRoleAsync(user, "Client");

                var client = new Client
                {
                    FullName = model.FullName,
                    UserID = user.Id
                };

                await _unitOfWork.Clients.AddAsync(client);
                await _unitOfWork.CompleteAsync();


                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }


        public async Task<SignInResult> LoginAsync(ClientLoginVM model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        }


        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }





      
        //provider
        public async Task<IdentityResult> RegisterProviderAsync(ProviderRegisterVM model)
        {
            
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                
                if (!await _roleManager.RoleExistsAsync("Provider"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Provider"));
                }
                await _userManager.AddToRoleAsync(user, "Provider");

                
                var provider = new Provider
                {
                    FullName = model.FullName,
                    UserId = user.Id,
                    Type = model.ProviderType 
                };

                
                await _unitOfWork.Providers.AddAsync(provider);
                await _unitOfWork.CompleteAsync();

                
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }
    }
}