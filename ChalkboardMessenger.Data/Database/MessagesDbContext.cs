using ChalkboardMessenger.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardMessenger.Data.Database
{
    public class MessagesDbContext : DbContext
    {
        public MessagesDbContext(DbContextOptions<MessagesDbContext> options) : base(options)
        {

        }
        public DbSet<MessageModel> Messages { get; set; }
    }
}
