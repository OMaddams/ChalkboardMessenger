using Microsoft.AspNetCore.Identity;

namespace ChalkboardMessenger.App.Services
{
    public class UserManager
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public UserManager(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task LogoutUser()
        {
            await signInManager.SignOutAsync();
        }
    }
}
