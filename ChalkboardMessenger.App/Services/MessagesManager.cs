using ChalkboardMessenger.Data.Models;
using ChalkboardMessenger.Data.Repo;

namespace ChalkboardMessenger.App.Services
{
    public class MessagesManager
    {
        private readonly IMessagesRepo repo;

        public MessagesManager(IMessagesRepo repo)
        {
            this.repo = repo;
        }

        public async Task<List<MessageModel>> GetAllMessageAsync()
        {
            return await repo.GetAllAsync();
        }
        public async Task<string> RemoveMessage(int id)
        {
            return await repo.RemoveAsync(id);

        }

        public async Task<MessageModel?> AddMessageAsync(string? username, string? message)
        {
            if (username == null || message == null)
            {
                return null;
            }
            return await repo.AddAsync(new MessageModel() { UserName = username, Message = message, Date = DateTime.UtcNow });
        }

        public async Task UpdateUsernamesAsync(string oldUsername, string newUsername)
        {
            await repo.UpdateUsernameAsync(oldUsername, newUsername);
            await repo.SaveChangesAsync();
        }
    }
}
