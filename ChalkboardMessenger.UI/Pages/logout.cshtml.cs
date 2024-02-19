using ChalkboardMessenger.App.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardMessenger.UI.Pages
{
    public class logoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager userManager;

        public logoutModel(SignInManager<IdentityUser> signInManager, UserManager userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnGet()
        {
            await userManager.LogoutUserAsync();
            return RedirectToPage("/Index");
        }
    }
}
