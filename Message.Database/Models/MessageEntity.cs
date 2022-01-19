using System;
using System.ComponentModel.DataAnnotations.Schema;
using Message.Common.Base;

namespace Message.Database.Models
{
    public class MessageEntity : BaseModel
    {
        public int SenderId { get; set; }
        public int ChatId { get; set; }
    }
}