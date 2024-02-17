using ChalkboardMessenger.App.Services;
using ChalkboardMessenger.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalkboardMessenger.UI.Pages.Member
{
    public class IndexModel : PageModel
    {
        private readonly MessagesManager messagesManager;
        [BindProperty]
        public string? Message { get; set; }
        public List<MessageModel> Messages { get; set; } = new();
        public string? Error { get; set; } = null;
        public IndexModel(MessagesManager messagesManager)
        {
            this.messagesManager = messagesManager;

        }
        public async Task OnGet(string error)
        {
            Messages = await messagesManager.GetAllMessage();
            Error = error;
        }

        public async Task<IActionResult> OnPost()
        {
            Error = null;
            MessageModel? response = await messagesManager.AddMessage(User.Identity.Name, Message);
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
