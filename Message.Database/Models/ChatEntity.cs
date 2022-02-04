using System;
using System.Collections.Generic;
using Message.Common.Base;

namespace Message.Database.Models
{
    public class ChatEntity : BaseModel
    {
        public string Title { get; set; }
        public List<Guid> Messages { get; set; }
        public List<Guid> Members { get; set; }
        public Guid? Admin { get; set; }
    }
}