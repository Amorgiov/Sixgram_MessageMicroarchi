using System;
using Message.Common.Base;
using Microsoft.AspNetCore.Http;

namespace Message.Core.Dto.Message
{
    public class CreateMessageDto : BaseModel
    {
        public IFormFile? File { get; set; }
        public Guid SenderId { get; set; }
        public string Text { get; set; }
    }
}