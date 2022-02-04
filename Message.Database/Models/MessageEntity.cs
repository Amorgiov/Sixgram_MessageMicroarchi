using System;
using System.ComponentModel.DataAnnotations.Schema;
using Message.Common.Base;

namespace Message.Database.Models
{
    public class MessageEntity : BaseModel
    {
        public Guid SenderId { get; set; }
        public string Text { get; set; }
        public Guid ChatId { get; set; }

        public ChatEntity Chat { get; set; }
    }
}