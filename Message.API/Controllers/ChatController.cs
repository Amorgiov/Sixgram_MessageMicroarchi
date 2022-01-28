using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Core.Dto.Chat;
using Message.Core.Services.Chat;
using Message.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Message.API.Controllers
{
    /// <summary>
    /// ChatManager
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("/api/[controller]/Room/")]
    public class ChatController : BaseController
    {
        private readonly IChatService _chatService;

        /// <inheritdoc />
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        
        /// <summary>
        /// Creating a chat room
        /// </summary>
        /// <param name="model"></param>
        /// <returns>New ChatEntity model</returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ChatDto>> CreateChat(ChatEntity model)
            => await ReturnResult<ResultContainer<ChatDto>, ChatDto>(_chatService.CreateChat(model));
        
        /// <summary>
        /// Deleting the chat room
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns>Deleted model</returns>
        [HttpPost("[action]/{chatId:int}")]
        public async Task<ActionResult<ChatDto>> DeleteChat(int chatId)
            => await ReturnResult<ResultContainer<ChatDto>, ChatDto>(_chatService.DeleteChat(chatId));

        /// <summary>
        /// Editing selected chat
        /// </summary>
        /// <param name="model"></param>
        /// <param name="chatId"></param>
        /// <returns>Edited model</returns>
        [HttpPut("editing")]
        public async Task<ActionResult<ChatDto>> EditChat(ChatDto model)
            => await ReturnResult<ResultContainer<ChatDto>, ChatDto>(_chatService.EditChat(model));
        
        /// <summary>
        /// Pull chat with id - %
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ChatEntity model</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ChatDto>> GetChatById(int id)
            => await ReturnResult<ResultContainer<ChatDto>, ChatDto>(_chatService.GetChatById(id));
    }
}