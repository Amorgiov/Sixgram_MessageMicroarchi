using System.Collections.Generic;
using Message.Common.Base;

namespace Message.Core.Dto
{
    public class MessageDto : BaseModel
    {
        public string Text { get; set; }
        public int SenderId { get; set; }
        public int ChatId { get; set; }
    }
}