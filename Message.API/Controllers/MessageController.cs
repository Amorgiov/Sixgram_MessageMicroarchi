using System;
using System.Threading.Tasks;
using Message.Common.Result;
using Message.Core.Dto.Chat;
using Message.Core.Dto.Message;
using Message.Core.Dto.Update;
using Message.Core.Services.Message;
using Message.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Message.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/")]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageService;
        private const long MaxFileSize = 2L * 1024L * 1024L * 1024L;
        /// <inheritdoc />
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        
        /// <summary>
        /// Creating messages and files
        /// </summary>
        /// <param name="data"></param>
        /// <param name="chatId"></param>
        /// <response code="204">Success</response>
        /// <response code="400">There is no file in the request</response>
        [HttpPost("{chatId:guid}/messages")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(MaxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
        public async Task<ActionResult> AddMessage([FromForm] CreateMessageDto data, Guid chatId)
            => await ReturnResult(_messageService.AddMessage(data, chatId));
        
        /// <summary>
        /// Get message by mesId
        /// </summary>
        /// <param name="mesId"></param>
        /// <response code="204">Success</response>
        /// <response code="400">There is no file in the request</response>
        [HttpGet("messages/{mesId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<CreateMessageDto>> GetMessageById(Guid mesId)
            => await ReturnResult<ResultContainer<CreateMessageDto>, CreateMessageDto>
                (_messageService.GetMessageById(mesId));
        
        /// <summary>
        /// Delete messages by mesId
        /// </summary>
        /// <param name="mesId"></param>
        /// <returns></returns>
        [HttpGet("messages/{mesId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(Guid mesId)
            => await ReturnResult(_messageService.Delete(mesId));
    }
}