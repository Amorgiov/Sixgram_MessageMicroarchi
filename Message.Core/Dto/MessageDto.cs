using System.Collections.Generic;

namespace Message.Core.Dto
{
    public class MessageDto
    {
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public string Message { get; set; }
        
        /*public string attachment { get; set; } media object*/
        
        public int ReplyTo { get; set; }
        public List<int> ForwardMessages { get; set; } //ReplyTo
    }
}