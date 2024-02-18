using ChalkboardMessenger.Data.Models;

namespace ChalkboardMessenger.Data.Repo
{
    public interface IMessagesRepo
    {


        public Task<MessageModel?> GetAsync(int Id);


        public Task<List<MessageModel>> GetAllAsync();

        public Task<List<MessageModel>> GetAllByUserAsync(string username);


        public Task<MessageModel> AddAsync(MessageModel modelToAdd);

        public Task<string> Remove(int id);

        public Task SaveChanges();

        public Task UpdateUsername(string oldUsername, string newUsername);
    }
}
