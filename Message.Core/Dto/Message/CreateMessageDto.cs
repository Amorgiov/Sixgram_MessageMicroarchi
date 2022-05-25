using System;
using Message.Common.Base;
using Microsoft.AspNetCore.Http;

namespace Message.Core.Dto.Message
{
    public class CreateMessageDto
    {
        public IFormFile? File { get; set; }
        public Guid SenderId { get; set; }
        public string Text { get; set; }
        
    }
}