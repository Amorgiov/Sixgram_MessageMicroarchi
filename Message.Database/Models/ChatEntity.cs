using System;
using System.Collections.Generic;
using Message.Common.Base;

namespace Message.Database.Models
{
    public class ChatEntity : BaseModel
    {
        public string Title { get; set; }
        public Guid? Admin { get; set; }
        
        public ICollection<MemberEntity> Members { get; set; }
        public ICollection<MessageEntity> Messages { get; set; }
    }
}