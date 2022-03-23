using System;
using Message.Common.Base;
using Microsoft.AspNetCore.Http;

namespace Message.Core.Dto.Message
{
    public class MessageDto : BaseModel
    {
        public string Text { get; set; }
        public Guid SenderId { get; set; }
    }
}