using System;
using System.ComponentModel.DataAnnotations.Schema;
using Message.Common.Base;
using Microsoft.AspNetCore.Http;

namespace Message.Database.Models
{
    public class MessageEntity : BaseModel
    {
        public Guid? SenderId { get; set; }
        public Guid? FileId { get; set; }
        public string Text { get; set; }
        public Guid ChatId { get; set; }

        public ChatEntity Chat { get; set; }
    }
}