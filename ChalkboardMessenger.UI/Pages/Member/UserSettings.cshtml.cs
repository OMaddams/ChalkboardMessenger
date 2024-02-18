using ChalkboardMessenger.App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardMessenger.UI.Pages.Member
{
    [BindProperties]
    public class UserSettingsModel : PageModel
    {
        private readonly UserManager userManager;

        public string? NewUsername { get; set; } = "";
        public string? NewPassword { get; set; }
        public string? CurrentUser { get; set; }
        public string? Password { get; set; }
        public string? Error { get; set; }


        public UserSettingsModel(UserManager userManager)
        {
            this.userManager = userManager;
        }

        public void OnGet(string error)
        {
            CurrentUser = HttpContext.User.Identity.Name;
            Error = error;
        }
        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrEmpty(Password)) return RedirectToPage("/Member/UserSettings", new { error = "Please fill out current password" });

            bool changedPassword = false;
            if (!string.IsNullOrEmpty(NewPassword))
            {
                Error = await userManager.ChangePassword(User.Identity.Name, NewPassword, Password);

                if (!string.IsNullOrEmpty(Error))
                {
                    return RedirectToPage("/Member/UserSettings", new { error = Error });
                }

                changedPassword = true;
            }
            if (NewUsername != null)
            {
                if (!string.IsNullOrEmpty(NewUsername.Trim()))
                {
                    Error = await userManager.ChangeUsername(User.Identity.Name, NewUsername, changedPassword ? NewPassword : Password);

                    if (!string.IsNullOrEmpty(Error))
                    {
                        return RedirectToPage("/Member/UserSettings", new { error = Error });
                    }
                }
            }


            return RedirectToPage("/Member/UserSettings");
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await userManager.DeleteUser(User.Identity.Name);
            return RedirectToPage("/Index");
        }
    }
}