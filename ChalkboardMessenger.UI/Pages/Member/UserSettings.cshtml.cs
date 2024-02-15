using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardMessenger.UI.Pages.Member
{
    public class UserSettingsModel : PageModel
    {
        public string? NewUsername { get; set; }
        public string? NewPassword { get; set; }
        public string? CurrentUser { get; set; }
        public string? Password { get; set; }

        public void OnGet()
        {
            CurrentUser = HttpContext.User.Identity.Name;
        }
        public void OnPost()
        {
            if (NewUsername != null || NewUsername != string.Empty)
            {

            }
        }
    }
}
