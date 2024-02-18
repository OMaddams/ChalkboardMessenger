using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ChalkboardMessenger.App.Services
{
    public class UserManager
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly MessagesManager messagesManager;

        public UserManager(SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, MessagesManager messagesManager)
        {
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.messagesManager = messagesManager;
        }
        public async Task LogoutUser()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<string> ChangePassword(string username, string newPassword, string oldPassword)
        {
            var userToChange = await signInManager.UserManager.FindByNameAsync(username);
            if (userToChange == null)
            {
                return "Couldn't find user";
            }
            var trySignIn = signInManager.CheckPasswordSignInAsync(userToChange, oldPassword, false);
            if (trySignIn.IsCompletedSuccessfully)
            {
                return "Wrong password";
            }

            var result = await signInManager.UserManager.ChangePasswordAsync(userToChange, oldPassword, newPassword);
            if (result.Succeeded)
            {
                return string.Empty;
            }

            return "Password needs to contain atleast 1 of each,  capital letter, number and symbol";
        }
        public async Task<string> ChangeUsername(string username, string newUsername, string password)
        {


            var userToChange = await signInManager.UserManager.FindByNameAsync(username);
            if (userToChange == null)
            {
                return "Couldn't find user";
            }
            var trySignIn = signInManager.CheckPasswordSignInAsync(userToChange, password, false);
            if (!trySignIn.IsCompletedSuccessfully)
            {
                return "Wrong password";
            }
            string oldUsername = userToChange.UserName;
            userToChange.UserName = newUsername;
            IdentityResult result = await signInManager.UserManager.UpdateAsync(userToChange);

            if (result.Succeeded)
            {
                await messagesManager.UpdateUsernames(oldUsername, newUsername);
                await signInManager.RefreshSignInAsync(userToChange);
                return "";
            }

            return "Error";


        }

        public async Task<bool> CheckAdmin(HttpContext httpContext)
        {
            IdentityUser? loggedInUser = await signInManager.UserManager.GetUserAsync(httpContext.User);
            if (loggedInUser == null)
            { return false; }
            await CreateAdminRole();
            return await signInManager.UserManager.IsInRoleAsync(loggedInUser, "Admin");




        }
        private async Task CreateAdminRole()
        {
            bool AdminRoleExists = await roleManager.RoleExistsAsync("Admin");


            if (!AdminRoleExists)
            {
                IdentityRole adminRole = new()
                {
                    Name = "Admin",
                };
                var createAdminRoleResult = await roleManager.CreateAsync(adminRole);

                if (createAdminRoleResult.Succeeded)
                {
                    AdminRoleExists = true;
                }
            }
        }

        public async Task DeleteUser(string user)
        {
            var result = await signInManager.UserManager.FindByNameAsync(user);
            if (result == null)
            {
                return;
            }
            await messagesManager.UpdateUsernames(result.UserName, "(Deleted)");
            await signInManager.SignOutAsync();
            await signInManager.UserManager.DeleteAsync(result);

        }
    }
}
