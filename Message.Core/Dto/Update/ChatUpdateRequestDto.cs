using System;
using System.Collections.Generic;
using Message.Common.Base;

namespace Message.Core.Dto.Update
{
    public class ChatUpdateRequestDto
    {
        public string Title { get; set; }
        public string NewTitle { get; set; }
        public List<Guid> NewMembers { get; set; }
        //something more 
    }
}