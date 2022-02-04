using System;
using System.Collections.Generic;
using System.Threading;
using Message.Common.Base;

namespace Message.Core.Dto.Chat
{
    public class ChatDto : BaseModel
    {
        public string Title { get; set; }
        public List<Guid> Members { get; set; }
        public Guid Admin { get; set; }
    }
}