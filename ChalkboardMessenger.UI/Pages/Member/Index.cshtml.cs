using ChalkboardMessenger.App.Services;
using ChalkboardMessenger.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardMessenger.UI.Pages.Member
{
    public class IndexModel : PageModel
    {
        private readonly MessagesManager messagesManager;
        private readonly UserManager userManager;

        [BindProperty]
        public string? Message { get; set; }
        public List<MessageModel> Messages { get; set; } = new();
        public string? Error { get; set; } = null;
        public bool IsAdmin { get; set; }
        public IndexModel(MessagesManager messagesManager, UserManager userManager)
        {
            this.messagesManager = messagesManager;
            this.userManager = userManager;
        }
        public async Task OnGet(string error)
        {
            Messages = await messagesManager.GetAllMessageAsync();
            Error = error;
            IsAdmin = await userManager.CheckAdminAsync(HttpContext);
        }

        public async Task<IActionResult> OnPost()
        {
            Error = null;
            MessageModel? response = await messagesManager.AddMessageAsync(User.Identity.Name, Message);
            if (response == null)
            {
                Error = "Cant post nothing";


            }

            return RedirectToPage("/Member/Index", new { error = Error });




        }

        public async Task<IActionResult> OnPostRemove(int cardId)
        {
            await messagesManager.RemoveMessage(cardId);

            return RedirectToPage("/Member/Index");
        }
    }
}
