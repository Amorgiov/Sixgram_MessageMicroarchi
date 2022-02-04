using System;
using System.Collections.Generic;
using Message.Common.Base;

namespace Message.Core.Dto
{
    public class MessageDto : BaseModel
    {
        public string Text { get; set; }
        public Guid SenderId { get; set; }
        public Guid ChatId { get; set; }
    }
}