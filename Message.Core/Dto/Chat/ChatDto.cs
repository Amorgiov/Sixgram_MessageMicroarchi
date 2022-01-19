using System.Collections.Generic;
using System.Threading;
using Message.Common.Base;

namespace Message.Core.Dto.Chat
{
    public class ChatDto : BaseModel
    {
        public string Title { get; set; }
        public List<int> Members { get; set; }
        public int Admin { get; set; }
    }
}