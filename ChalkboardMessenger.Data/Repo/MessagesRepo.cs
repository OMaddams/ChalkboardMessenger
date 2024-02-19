using ChalkboardMessenger.Data.Database;
using ChalkboardMessenger.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardMessenger.Data.Repo
{
    public class MessagesRepo : IMessagesRepo
    {
        private readonly MessagesDbContext context;

        public MessagesRepo(MessagesDbContext context)
        {
            this.context = context;
        }

        public async Task<MessageModel?> GetAsync(int Id)
        {
            return await context.Messages.FindAsync(Id);
        }


        public async Task<List<MessageModel>> GetAllAsync()
        {
            return await context.Messages.OrderByDescending(m => m.Date).ToListAsync();
        }
        public async Task<List<MessageModel>> GetAllByUserAsync(string username)
        {
            return await context.Messages.Where(m => m.UserName == username).ToListAsync();
        }

        public async Task<MessageModel> AddAsync(MessageModel modelToAdd)
        {
            await context.Messages.AddAsync(modelToAdd);
            await SaveChangesAsync();
            return modelToAdd;
        }

        public async Task UpdateUsernameAsync(string oldUsername, string newUsername)
        {
            await context.Messages.Where(m => m.UserName == oldUsername).ForEachAsync(m => m.UserName = newUsername);
        }
        public async Task<string> RemoveAsync(int id)
        {
            MessageModel? modelToRemove = await GetAsync(id);
            if (modelToRemove != null)
            {
                context.Messages.Remove(modelToRemove);
                await SaveChangesAsync();
                return "Removed successfully";
            }
            return "Could not find message";
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}