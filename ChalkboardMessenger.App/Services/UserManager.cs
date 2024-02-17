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
        public async Task<string> ChangeUsername(string username, string newUsername, string password)
        {


            var userToChange = signInManager.UserManager.FindByNameAsync(username);
            var trySignIn = signInManager.CheckPasswordSignInAsync(userToChange.Result, password, false);
            if (!trySignIn.IsCompletedSuccessfully)
            {
                return "Wrong";
            }
            if (userToChange.Result != null)
            {
                userToChange.Result.UserName = newUsername;
                IdentityResult result = await signInManager.UserManager.UpdateAsync(userToChange.Result);

                if (result.Succeeded)
                {
                    return "/Member/Index";
                }

                return "/Error";
            }
            return "/Error";
        }
    }
}
