using System;
using System.Collections.Generic;

namespace Message.Core.Dto.Update
{
    public class ChatUpdateResponseDto
    {
        public string Title { get; set; }
        public List<Guid> Members { get; set; }
    }
}