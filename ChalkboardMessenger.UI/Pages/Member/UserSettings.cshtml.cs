using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardMessenger.UI.Pages.Member
{
    public class UserSettingsModel : PageModel
    {
        public IdentityUser CurrentUser { get; set; }
        public void OnGet()
        {
        }
    }
}
