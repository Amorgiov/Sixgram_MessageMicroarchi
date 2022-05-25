using System;
using System.Threading.Tasks;
using Message.Common.Result;
using Message.Core.Dto.Chat;
using Message.Core.Dto.Update;
using Message.Core.Services.Chat;
using Message.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Message.API.Controllers
{
    /// <summary>
    /// ChatManager
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    [Route("/api/v{version:apiVersion}/[controller]/Room/")]
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ChatResponseDto>> CreateChat(ChatRequestDto model)
            => await ReturnResult<ResultContainer<ChatResponseDto>, ChatResponseDto>(_chatService.CreateChat(model));
        
        /// <summary>
        /// Deleting the chat room
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Deleted model</returns>
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteChat(Guid id)
            => await ReturnResult(_chatService.DeleteChat(id));

        /// <summary>
        /// Editing selected chat
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>Edited model</returns>
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ChatUpdateRequestDto>> EditChat(ChatUpdateRequestDto model, Guid id)
            => await ReturnResult<ResultContainer<ChatUpdateResponseDto>, ChatUpdateResponseDto>(_chatService.EditChat(model, id));
        
        /// <summary>
        /// Pull chat with id - %
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ChatEntity model</returns>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ChatResponseDto>> GetChatById(Guid id)
            => await ReturnResult<ResultContainer<ChatResponseDto>, ChatResponseDto>(_chatService.GetChatById(id));
    }
}