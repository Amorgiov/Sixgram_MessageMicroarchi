using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Message.Database.Models
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ChatId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}