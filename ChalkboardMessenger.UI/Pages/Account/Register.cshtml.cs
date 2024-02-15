using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardMessenger.UI.Pages.Account
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public RegisterModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            IdentityUser newUser = new()
            {
                UserName = Username,
            };

            var createUserResult = await signInManager.UserManager.CreateAsync(newUser, Password);

            if (createUserResult.Succeeded)
            {

                IdentityUser? userToLogIn = await signInManager.UserManager.FindByNameAsync(Username);

                var signInResult = await signInManager.PasswordSignInAsync(userToLogIn, Password, false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToPage("/Member/Index");
                }



            }
            else
            {

            }
            return Page();
            //signInManager.UserManager.GetUserAsync(HttpContext.User)
        }
    }
}
