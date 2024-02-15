using System.ComponentModel.DataAnnotations;

namespace ChalkboardMessenger.Data.Models
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
