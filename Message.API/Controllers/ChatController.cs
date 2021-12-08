using Message.Core.Dto;
using Message.Core.Services.Chat;
using Microsoft.AspNetCore.Mvc;

namespace Message.API.Controllers
{
    [ApiController]
    
    [Route("/api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IChatService _messageService;
        public ChatController(IChatService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("[action]")]
       
        public void SendMessage(MessageDto messageDto, int chatId)
        {
            
        }
    }
}