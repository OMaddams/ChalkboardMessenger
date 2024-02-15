using ChalkboardMessenger.App.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardMessenger.UI.Pages
{
    public class logoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public logoutModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet()
        {
            await new UserManager(signInManager).LogoutUser();
            return RedirectToPage("/Index");
        }
    }
}
