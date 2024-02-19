using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardMessenger.UI.Pages.Account
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public string Username { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public async Task<IActionResult> OnPost()
        {
            if (Username != null && Password != null)
            {
                IdentityUser? userToSignIn = await signInManager.UserManager.FindByNameAsync(Username);

                if (userToSignIn != null)
                {
                    var signInResult = await signInManager.PasswordSignInAsync(userToSignIn, Password, false, false);

                    if (signInResult.Succeeded)
                    {
                        return RedirectToPage("/Member/Index");
                    }
                }
            }

            return RedirectToPage("/Account/Login", new { errorMessage = "Your credentials didn't match" });
        }

    }
}
