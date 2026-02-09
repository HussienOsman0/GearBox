using BLL.View_models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterFaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterClientAsync(ClientRegisterVM model);


        Task<SignInResult> LoginAsync(ClientLoginVM model);

        Task SignOutAsync();

        Task<IdentityResult> RegisterProviderAsync(ProviderRegisterVM model);
    }
}
