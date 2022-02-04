using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Message.Common.Result;
using Message.Core.Dto;
using Message.Core.Dto.Chat;
using Message.Core.Dto.Update;
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
        [HttpPost("[action]/{chatId:Guid}")]
        public async Task<ActionResult<ChatDto>> DeleteChat(Guid chatId)
            => await ReturnResult<ResultContainer<ChatUpdateResponseDto>, ChatUpdateResponseDto>(_chatService.DeleteChat(chatId));

        /// <summary>
        /// Editing selected chat
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>Edited model</returns>
        [HttpPut("{id:Guid}/editing")]
        public async Task<ActionResult<ChatUpdateRequestDto>> EditChat(ChatUpdateRequestDto model, Guid id)
            => await ReturnResult<ResultContainer<ChatUpdateResponseDto>, ChatUpdateResponseDto>(_chatService.EditChat(model, id));
        
        /// <summary>
        /// Pull chat with id - %
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ChatEntity model</returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ChatDto>> GetChatById(Guid id)
            => await ReturnResult<ResultContainer<ChatDto>, ChatDto>(_chatService.GetChatById(id));
    }
}