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
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
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
        /// <response code="404">Message not found</response>
        /// <response code="204">Success</response>
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
        /// <response code="404">Message not found</response>
        /// <response code="200">Success</response>
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
        /// <response code="404">Message not found</response>
        /// <response code="200">Success</response>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ChatResponseDto>> GetChatById(Guid id)
            => await ReturnResult<ResultContainer<ChatResponseDto>, ChatResponseDto>(_chatService.GetChatById(id));
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">Message not found</response>
        /// <response code="204">Success</response>
        /// <response code="400">There is no file in the request</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("{chatId:guid}/[action]/{userId:guid}")]
        public async Task<ActionResult> AddMember(Guid userId, Guid chatId)
            => await ReturnResult(_chatService.AddMember(userId, chatId));
    }
}