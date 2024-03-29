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
        public string PasswordRepeat { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public async Task<IActionResult> OnPost()
        {
            if (Username.ToUpper() == "ADMIN")
            {
                return Page();
            }

            IdentityUser newUser = new()
            {
                UserName = Username,
            };

            try
            {
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

                return RedirectToPage("/Account/Register", new { errorMessage = "Could not create account, already existing username or Invalid password" });
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Account/Register", new { errorMessage = "Could not create account, already existing username or Invalid password" });
            }




            //signInManager.UserManager.GetUserAsync(HttpContext.User)
        }


    }
}
